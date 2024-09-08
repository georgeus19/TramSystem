using MissionPlanning.Api.TrackFinders;
using TrackTramControl.Api;
using Utils;

namespace MissionPlanning.Api.TrackGraphFinders; 

/// <summary>
/// Abstract class for finding free tram to plan a mission for in a track graph. Supports
/// adding strategies for how to find such a tram for one track (=graph vertex).
/// </summary>
public abstract class StrategyMissionTrackGraphFinder : MissionTrackGraphFinder {
	protected MissionTrackFinder MissionTrackFinder { get; private set; }

	protected StrategyMissionTrackGraphFinder(MissionTrackFinder trackFinder) {
		MissionTrackFinder = trackFinder;
	}
	
	public void SetTrackFinderStrategy(MissionTrackFinder trackFinder) {
		MissionTrackFinder = trackFinder;
	}

	public abstract TramId? FindFreeTram(ReadableTrackGraph trackGraph);
}