using MissionPlanning.Api;
using TrackTramControl.Api;
using Utils;

namespace MissionPlanning.Implementation; 

public class BinarySearchTrackFinder : MissionTrackFinder {
	private IDictionary<TramId, Mission> _tramMissions;
	
	
	public BinarySearchTrackFinder(IDictionary<TramId, Mission> tramMissions) {
		_tramMissions = tramMissions;
	}
	
	public TramId FindFreeTram(ReadableTrackVertex track) {
		throw new NotImplementedException();
	}
}