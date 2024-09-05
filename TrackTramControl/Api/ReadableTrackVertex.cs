using Utils;

namespace TrackTramControl.Api; 

/// <summary>
/// Vertex in <see cref="ReadableTrackGraph"/> representing a track. A track here means a block between two switches or track ends.
/// </summary>
public interface ReadableTrackVertex {
	public TrackId ID { get; }
	public IReadOnlyList<TramId> GetTrams();
	
	/// <summary>
	/// Since each track always ends with switches or track ends, it can have adjacent tracks on both two sides which
	/// are provided by this function.
	/// </summary>
	public (IReadOnlyList<ReadableTrackVertex>, IReadOnlyList<ReadableTrackVertex>) GetAdjacentTracks();
}