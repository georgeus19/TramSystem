using MissionPlanning.Api.TrackFinders;
using TrackTramControl.Api;
using Utils;

namespace MissionPlanning.Api.TrackGraphFinders; 

/// <summary>
/// This implementation only searches for the free tram in the root track of a track graph.
/// </summary>
public sealed class RootTrackFinder : StrategyMissionTrackGraphFinder {
	public RootTrackFinder(MissionTrackFinder trackFinder): base(trackFinder) {	}
	
	public override TramId? FindFreeTram(ReadableTrackGraph trackGraph) {
		return MissionTrackFinder.FindFreeTram(trackGraph.GetRootTrack());
	}
}