using MissionPlanning.Api;
using TrackTramControl.Api;
using Utils;

namespace MissionPlanning.Implementation; 

/// <summary>
/// Implements a binary search algorithm to find the "first" tram without a mission on a given track.
/// </summary>
public class BinarySearchTrackFinder : MissionTrackFinder {
	private readonly IDictionary<TramId, Mission> _tramMissions;
	private int c = 0;
	
	public BinarySearchTrackFinder(IDictionary<TramId, Mission> tramMissions) {
		_tramMissions = tramMissions;
	}
	
	public TramId? FindFreeTram(ReadableTrackVertex track) {
		IReadOnlyList<TramId> trams = track.GetTrams();
		Console.WriteLine("BinarySearch");
		return BinarySearch(trams, new SearchRange(From: 0, To: trams.Count));
	}

	/// <summary>
	/// Binary search algorithm for finding a tram without a mission. Range parameter must be constructed so
	/// that `From` is index of first range element and `To` is index of element right after the searched range (e.g. `trams.Count`).
	/// </summary>
	private TramId? BinarySearch(IReadOnlyList<TramId> trams, SearchRange range) {
		Console.WriteLine($"Range {range.From} {range.To} C: {++c}");
		
		// There are no trams or all trams have a mission already.
		if (range.From == range.To) {
			return null;
		}

		if (!_tramMissions.ContainsKey(trams[range.From])) {
			return trams[range.From];
		}
		
		int halfRangeSize = (range.To - range.From) / 2;
		int halfRangeIndex = range.From + halfRangeSize; // Is always less than `range.To` since `From`!=`To` 
		if (_tramMissions.ContainsKey(trams[halfRangeIndex])) {
			// Search the latter half since all trams in the former half have missions.
			return BinarySearch(trams, new SearchRange(From: halfRangeIndex + 1, To: range.To));
		} else {
			// Search the former half since there is at least one tram without the mission (the one in half).
			TramId? tram = BinarySearch(trams, new SearchRange(From: range.From, To: halfRangeIndex));
			return tram ?? trams[halfRangeIndex];
		}
	}

	private record SearchRange(int From, int To);
}