using MissionPlanning.Implementation;
using TrackTramControl.Api;
using Utils;

namespace MissionPlanning.Api.TrackGraphFinders; 

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