using TrackTramControl.Api;

namespace Frontend.Services.Depot; 

public class TramPositionDto {
	public string TrackID { get; set; }
	public int Position { get; set; }

	public TramPositionDto(string trackID, int position) {
		TrackID = trackID;
		Position = position;
	}

	public TramPosition ToTramPosition() {
		return new TramPosition(TrackId.From(TrackID), Position);
	}
}
