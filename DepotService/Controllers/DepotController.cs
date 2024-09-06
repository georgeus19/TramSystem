using DepotService.Controllers.Dto;
using DepotService.StorageAccess;
using Microsoft.AspNetCore.Mvc;
using TrackTramControl.Api;

namespace DepotService.Controllers; 

/// <summary>
/// Endpoint controller for managing the single existing depot. Currently, it is only possible to take out its tracks.
/// It would not contain additional data; therefore, even its class is not materialized in code.
/// </summary>
[Route("api/")]
public class DepotController : Controller {
	private readonly TrackGraphRepository _trackGraphRepository;
	private readonly ILogger<DepotController> _logger;

	public DepotController(TrackGraphRepository trackGraphRepository, ILogger<DepotController> logger) {
		_trackGraphRepository = trackGraphRepository;
		_logger = logger;
	}
	
	[Route("depot")]
	[HttpGet]
	public async Task<DepotDto> Get() {
		_logger.LogInformation($"Depot Get");
		ReadableTrackGraph trackGraph = await _trackGraphRepository.Get();
		return new DepotDto(trackGraph.Serialize());
	}
}