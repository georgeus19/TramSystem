using MissionPlanning.Implementation;
using TrackTramControl.Api;
using Utils;

namespace MissionPlanning.Api.TrackGraphFinders; 

public sealed class RootTrackFinder : StrategyMissionTrackGraphFinder {
	public RootTrackFinder(MissionTrackFinder trackFinder): base(trackFinder) {	}
	
	public override TramId FindFreeTram(ReadableTrackGraph trackGraph) {
		return MissionTrackFinder.FindFreeTram(trackGraph.GetRootTrack());
	}
}