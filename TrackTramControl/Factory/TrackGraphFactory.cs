using System.Net;

namespace TrackTramControl.Factory; 

public class TrackGraphFactory {
	public static ReadableTrackGraph CreateReadableTrackGraph(string? serialization) {
		if (serialization != null) {
			return TrackGraph.Create(serialization);	
		}
		return new TrackGraph();

	}

	public static ModifiableTrackGraph CreateModifiableTrackGraph(string? serialization) {
		if (serialization != null) {
			return TrackGraph.Create(serialization);
		}
		return new TrackGraph();
	}
	
}