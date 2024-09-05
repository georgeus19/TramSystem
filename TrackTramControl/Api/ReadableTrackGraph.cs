namespace TrackTramControl.Api; 

/// <summary>
/// One of the main interfaces for using from public domains. It provides a view of trams parked on a structure made of
/// tracks and switches represented in a form of a graph with tracks as vertices and edges being eligible tram transitions between tracks.
///
/// <see cref="ReadableTrackGraph"/> for track representation within the graph.
/// </summary>
public interface ReadableTrackGraph : SerializableTrackGraph {
	public ReadableTrackVertex GetRootTrack();
}