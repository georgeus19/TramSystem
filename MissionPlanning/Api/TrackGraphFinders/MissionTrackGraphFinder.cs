using TrackTramControl.Api;
using Utils;

namespace MissionPlanning.Api.TrackGraphFinders; 

public interface MissionTrackGraphFinder {
	public TramId? FindFreeTram(ReadableTrackGraph trackGraph);
}