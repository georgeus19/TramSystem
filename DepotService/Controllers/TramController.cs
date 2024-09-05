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

	public TramController(TrackGraphRepository trackGraphRepository) {
		_trackGraphRepository = trackGraphRepository;
	}
	
	/// <summary>
	/// Endpoint for adding trams to depot. Usually, it would take tram ID as a parameter
	/// but there is no service that would take care of trams and assign IDs and no additional tram data; therefore,
	/// trams.
	/// </summary>
	/// <returns></returns>
	[Route("trams")]
	[HttpPost]
	public async Task<DepotDto> AddTram([FromBody]TramPositionDto tramPosition) {
		var tramId = TramId.NewId();
		ModifiableTrackGraph trackGraph = await _trackGraphRepository.Get();
		trackGraph.AddTram(tramId, tramPosition.ToTramPosition());
		var updatedTrackGraph = await _trackGraphRepository.Update(trackGraph);
		return new DepotDto(updatedTrackGraph.Serialize());
	}
}