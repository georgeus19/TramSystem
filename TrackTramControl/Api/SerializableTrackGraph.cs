namespace TrackTramControl.Api; 

/// <summary>
/// Interface for serialization which lets track graph be serialized. The serialization can be put in a track
/// graph factory method <see cref="TrackGraphFactory"/> to reconstruct the graph.
/// </summary>
public interface SerializableTrackGraph {
	public string Serialize();
}