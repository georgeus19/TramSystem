using TrackTramControl.Api;
using Utils;

namespace MissionPlanning.Implementation; 

public interface MissionTrackFinder {
	public TramId FindFreeTram(ReadableTrackVertex track);
}