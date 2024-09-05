using Utils;

namespace TrackTramControl.Api;

/// <summary>
/// One of the main interfaces for using from public domains. It provides a view of trams parked on a structure made of
/// tracks and switches represented in a form of a graph with tracks as vertices and edges being eligible tram transitions between tracks.
///
/// This interface also allows modification of the structure. For readonly version, use <see cref="ReadableTrackGraph"/>.
/// </summary>
public interface ModifiableTrackGraph : ReadableTrackGraph {
	public void AddTram(TramId tram, TramPosition position);
	public ReadableTrackVertex AddLeftTrack(TrackId adjacentTrack);
	public ReadableTrackVertex AddRightTrack(TrackId adjacentTrack);
}