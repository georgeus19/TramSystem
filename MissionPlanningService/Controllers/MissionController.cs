using DepotService.Controllers.Dto;
using Microsoft.AspNetCore.Mvc;
using MissionPlanning.Api;
using MissionPlanning.Api.TrackGraphFinders;
using MissionPlanning.Implementation;
using MissionPlanningService.Controllers.Dto;
using TrackTramControl.Api;
using TrackTramControl.Api.Factory;

namespace MissionPlanningService.Controllers; 

[Route("api/")]
public class MissionController : Controller {
	private MissionRepository _missionRepository;
	private HttpClient _httpClient;

	public MissionController(MissionRepository missionRepository, HttpClient httpClient) {
		_missionRepository = missionRepository;
		_httpClient = httpClient;
	}
	
	[Route("missions")]
	[HttpGet]
	public async Task<IEnumerable<MissionDto>> Get() {
		IEnumerable<Mission> missions = await _missionRepository.GetMissions();
		return missions.Select(m => new MissionDto(m)).ToList();
	}
	
	/// <summary>
	/// Endpoint for adding trams to depot. Usually, it would take tram ID as a parameter
	/// but there is no service that would take care of trams and assign IDs and no additional tram data; therefore,
	/// trams.
	/// </summary>
	/// <returns></returns>
	[Route("missions")]
	[HttpPost]
	public async Task<IActionResult> PlanMission() {
		DepotDto? depot =  await _httpClient.GetFromJsonAsync<DepotDto>("http://localhost:5052/api/depot");
		if (depot == null) {
			return BadRequest();
		}
		ReadableTrackGraph trackGraph = TrackGraphFactory.CreateReadableTrackGraph(depot.TrackGraph); 
		IEnumerable<Mission> missions = await _missionRepository.GetMissions();
		var trackGraphFinder = new RootTrackFinder(new BinarySearchTrackFinder(missions.ToDictionary(m => m.TramID, m => m)));
		Mission plannedMission = new MissionPlanner().PlanMission(trackGraph: trackGraph, missionTrackGraphFinder: trackGraphFinder);
		var savedMission = await _missionRepository.Create(plannedMission);
		return Ok(new MissionDto(savedMission));
	}
}