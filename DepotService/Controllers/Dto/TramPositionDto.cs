using TrackTramControl.Api;

namespace DepotService.Controllers.Dto; 

public class TramPositionDto {
	public string TrackID { get; set; }
	public int Position { get; set; }

	public TramPositionDto(string trackID, int position) {
		TrackID = trackID;
		Position = position;
	}

	public TramPositionDto() {
		
	}

	public TramPosition ToTramPosition() {
		return new TramPosition(TrackId.From(TrackID), Position);
	}
}