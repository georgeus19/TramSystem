using TrackTramControl.Api;
using Utils;

namespace MissionPlanning.Api.TrackFinders; 

/// <summary>
/// Interface for finding a suitable free tram to plan a mission for given trams on a single track.
/// </summary>
public interface MissionTrackFinder {
	public TramId? FindFreeTram(ReadableTrackVertex track);
}