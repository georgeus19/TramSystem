using TrackTramControl.Api;

namespace Frontend.Services.Depot; 

public class Depot {
	public ReadableTrackGraph TrackGraph { get; }

	public Depot(ReadableTrackGraph trackGraph) {
		TrackGraph = trackGraph;
	}
}