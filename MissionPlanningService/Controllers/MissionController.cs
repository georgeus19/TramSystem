using Microsoft.AspNetCore.Mvc;
using MissionPlanning.Api;
using MissionPlanning.Api.TrackFinders;
using MissionPlanning.Api.TrackGraphFinders;
using MissionPlanningService.Controllers.Dto;
using MissionPlanningService.Lock;
using MissionPlanningService.Lock.Operations;
using TrackTramControl.Api;
using TrackTramControl.Api.Factory;

namespace MissionPlanningService.Controllers; 

[Route("api/")]
public class MissionController : Controller {
	private readonly MissionRepository _missionRepository;
	private readonly HttpClient _httpClient;
	private readonly ILogger<MissionController> _logger;
	private readonly MissionPlanningLock _missionPlanningLock;
	private readonly IConfiguration _configuration;

	public MissionController(MissionRepository missionRepository, HttpClient httpClient, ILogger<MissionController> logger, MissionPlanningLock missionPlanningLock, IConfiguration configuration) {
		_missionRepository = missionRepository;
		_httpClient = httpClient;
		_logger = logger;
		_missionPlanningLock = missionPlanningLock;
		_configuration = configuration;
	}
	
	/// <summary>
	/// Endpoint for retrieving all planned missions.
	/// </summary>
	[Route("missions")]
	[HttpGet]
	public async Task<IActionResult> Get() {
		_logger.LogInformation("Missions Get");
		IEnumerable<MissionDto>? resultingMissions = await _missionPlanningLock.RunOperation(new GetMissionsOperation<IEnumerable<MissionDto>>(
			operationCode: async () => {
				IEnumerable<Mission> missions = await _missionRepository.GetMissions();
				return missions.Select(m => new MissionDto(m)).ToList();
			}));
		
		if (resultingMissions != null) {
			return Ok(resultingMissions);
		} else {
			return Conflict("Another user is planning missions currently.");
		}
	}
	
	/// <summary>
	/// Endpoint for planning missions - adding a mission to the most suitable tram.
	/// </summary>
	[Route("missions")]
	[HttpPost]
	public async Task<IActionResult> PlanMission() {
		_logger.LogInformation("PlanMission");
		var result = await _missionPlanningLock.RunOperation(new PlanMissionOperation<IActionResult?>(async () => {
			DepotDto? depot = await _httpClient.GetFromJsonAsync<DepotDto>($"{GetDepotServiceUrl()}/api/depot");
			if (depot == null) {
				return StatusCode(504);
			}

			ReadableTrackGraph trackGraph = TrackGraphFactory.CreateReadableTrackGraph(depot.TrackGraph);
			IEnumerable<Mission> missions = await _missionRepository.GetMissions();
			var trackGraphFinder =
				new RootTrackFinder(new BinarySearchTrackFinder(missions.ToDictionary(m => m.TramID, m => m)));
			Mission? plannedMission =
				new MissionPlanner().PlanMission(trackGraph: trackGraph, missionTrackGraphFinder: trackGraphFinder);
			
			if (plannedMission == null) {
				return BadRequest("Mission could not be planned. There is probably no tram without a mission.");
			}
			
			var savedMission = await _missionRepository.Create(plannedMission);
			return Ok(new MissionDto(savedMission));
		}));

		if (result != null) {
			return result;
		} else {
			return Conflict("Another user is planning missions currently.");
		}
	}

	private string GetDepotServiceUrl() {
		string? serviceUrl = _configuration.GetSection("Services").GetSection("DepotServiceUrl").Value;
		if (string.IsNullOrEmpty(serviceUrl)) {
			throw new Exception("Depot Service Url is not configured.");
		}

		return serviceUrl;
	}
}