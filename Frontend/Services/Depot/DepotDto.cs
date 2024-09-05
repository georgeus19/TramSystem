using TrackTramControl.Api.Factory;

namespace Frontend.Services.Depot; 

public class DepotDto {
	public string TrackGraph { get; set; }

	public DepotDto(string trackGraph) {
		TrackGraph = trackGraph;
	}

	public DepotDto() {
		TrackGraph = "";
	}

	public Depot ToDepot() {
		return new Depot(TrackGraphFactory.CreateReadableTrackGraph(TrackGraph));
	}
}