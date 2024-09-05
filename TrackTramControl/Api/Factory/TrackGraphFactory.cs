using TrackTramControl.Implementation;

namespace TrackTramControl.Api.Factory; 

/// <summary>
/// This factory class provides the only way how to create track graphs from outside the library.
///
/// It either constructs an empty track graph or from a serialized string created from Serialize method of <see cref="SerializableTrackGraph"/>.
/// </summary>
public class TrackGraphFactory {
	public static ReadableTrackGraph CreateReadableTrackGraph(string? serialization = null) {
		if (serialization != null) {
			return TrackGraph.Create(serialization);	
		}
		return new TrackGraph();

	}

	public static ModifiableTrackGraph CreateModifiableTrackGraph(string? serialization = null) {
		if (serialization != null) {
			return TrackGraph.Create(serialization);
		}
		return new TrackGraph();
	}
	
}