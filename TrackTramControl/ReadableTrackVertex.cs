using System.Data.Common;
using Utils;

namespace TrackTramControl; 

public interface ReadableTrackVertex {
	public TrackId ID { get; }
	public IReadOnlyList<TramId> GetTrams();
	public (IReadOnlyList<ReadableTrackVertex>, IReadOnlyList<ReadableTrackVertex>) GetAdjacentTracks();
}