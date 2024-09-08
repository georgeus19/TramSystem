using TrackTramControl.Api;
using Utils;

namespace MissionPlanning.Api.TrackGraphFinders; 

/// <summary>
/// Interface for finding the most suitable tram to plan the next mission for given the whole track graph (depot).
/// </summary>
public interface MissionTrackGraphFinder {
	public TramId? FindFreeTram(ReadableTrackGraph trackGraph);
}