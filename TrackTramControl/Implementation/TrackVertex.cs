using TrackTramControl.Api;
using Utils;

namespace TrackTramControl.Implementation; 

/// <summary>
/// Internal representation of a track within a track graph. This class should never be used directly outside the library.
/// </summary>
internal class TrackVertex : ReadableTrackVertex {
	public TrackId ID { get; }
	private List<TramId> _trams;
	private readonly List<TrackVertex> _leftAdjacentTracks;
	private readonly List<TrackVertex> _rightAdjacentTracks;

	internal TrackVertex(TrackId id, IEnumerable<TramId> trams, IEnumerable<TrackVertex> leftAdjacent, IEnumerable<TrackVertex> rightAdjacent) {
		ID = id;
		_trams = new List<TramId>(trams);
		_leftAdjacentTracks = new List<TrackVertex>(leftAdjacent);
		_rightAdjacentTracks = new List<TrackVertex>(rightAdjacent);
	}
	
	internal TrackVertex(TrackVertex other) {
		ID = other.ID;
		_trams = other._trams;
		_leftAdjacentTracks = other._leftAdjacentTracks;
		_rightAdjacentTracks = other._rightAdjacentTracks;
	}
	
	public IReadOnlyList<TramId> GetTrams() {
		return _trams;
	}

	public (IReadOnlyList<ReadableTrackVertex>, IReadOnlyList<ReadableTrackVertex>) GetAdjacentTracks() {
		return (_leftAdjacentTracks, _rightAdjacentTracks);
	}

	internal void AddTram(TramId tram, TramPosition position) {
		if (position.Position >= _trams.Count || position.Position < 0) {
			_trams.Add(tram);
		} else {
			var before = _trams.Slice(0, position.Position);
			var after = _trams.Slice(position.Position, _trams.Count);
			_trams = before.Concat(new[] { tram }).Concat(after).ToList();
		}
	}

	internal void AddLeftTrack(TrackVertex track) {
		_leftAdjacentTracks.Add(track);
	}

	internal void AddRightTrack(TrackVertex track) {
		_rightAdjacentTracks.Add(track);
	}

	internal TrackVertexJson ToJsonVersion() {
		return new TrackVertexJson(id: ID, trams: _trams,
			leftAdjacent: _leftAdjacentTracks.Select(t => t.ToJsonVersion()),
			rightAdjacent: _rightAdjacentTracks.Select(t => t.ToJsonVersion()));
	}
}