using DepotService.Controllers.Dto;
using DepotService.StorageAccess;
using Microsoft.AspNetCore.Mvc;
using TrackTramControl.Api;
using Utils;

namespace DepotService.Controllers; 

/// <summary>
/// Endpoint controller for managing trams within the depot. 
/// </summary>
[Route("api/depot/")]
public class TramController : Controller {
	private readonly TrackGraphRepository _trackGraphRepository;
	private readonly ILogger<TramController> _logger;

	public TramController(TrackGraphRepository trackGraphRepository, ILogger<TramController> logger) {
		_trackGraphRepository = trackGraphRepository;
		_logger = logger;
	}
	
	/// <summary>
	/// Endpoint for adding trams to depot. Usually, it would take tram ID as a parameter
	/// but there is no service that would take care of trams and assign IDs and no additional tram data; therefore,
	/// trams.
	/// </summary>
	/// <returns></returns>
	[Route("trams")]
	[HttpPost]
	public async Task<IActionResult> AddTram([FromBody]TramPositionDto tramPosition) {
		_logger.LogInformation($"AddTram position: Track: {tramPosition.TrackID} Position:{tramPosition.Position}", tramPosition);
		var tramId = TramId.NewId();
		ModifiableTrackGraph trackGraph = await _trackGraphRepository.Get();
		trackGraph.AddTram(tramId, tramPosition.ToTramPosition());
		var updatedTrackGraph = await _trackGraphRepository.Update(trackGraph);
		return Ok(new DepotDto(updatedTrackGraph.Serialize()));
	}
}