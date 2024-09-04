using Utils;
using System.Text.Json;
namespace TrackTramControl; 

public class TrackGraph : ReadableTrackGraph, ModifiableTrackGraph {
	private TrackVertex _rootTrack;
	private readonly Dictionary<TrackId, TrackVertex> _tracks;

	private TrackGraph(TrackVertex rootTrack, Dictionary<TrackId, TrackVertex> tracks) {
		_rootTrack = rootTrack;
		_tracks = tracks;
	}

	internal TrackGraph() {
		_rootTrack = new TrackVertex(id: TrackId.NewId(), trams: new TramId[] { }, leftAdjacent: new TrackVertex[] { }, rightAdjacent: new TrackVertex[] { });
		_tracks = new Dictionary<TrackId, TrackVertex>();
	}
	
	#region public

	#region query
	
	public string Serialize() {
		return JsonSerializer.Serialize(new TrackVertex[]{_rootTrack}.Concat(_tracks.Values).ToArray());
	}

	public ReadableTrackVertex GetRootTrack() {
		return _rootTrack;
	}

	#endregion
	
	#region mutation
	
	public void AddTram(TramId tram, TramPosition position) {
		GetTrack(position.TrackID).AddTram(tram, position);
	}

	public ReadableTrackVertex AddLeftTrack(TrackId adjacentTrack) {
		var t = new TrackVertex(id: TrackId.NewId(), trams: new TramId[] { }, leftAdjacent: new TrackVertex[] { },
			rightAdjacent: new TrackVertex[] { });
		GetTrack(adjacentTrack).AddLeftTrack(t);
		return t;
	}
	
	public ReadableTrackVertex AddRightTrack(TrackId adjacentTrack) {
		var t = new TrackVertex(id: TrackId.NewId(), trams: new TramId[] { }, leftAdjacent: new TrackVertex[] { },
			rightAdjacent: new TrackVertex[] { });
		GetTrack(adjacentTrack).AddRightTrack(t);
		return t;
	}
	
	#endregion
	
	#endregion
	
	#region internal

	internal static TrackGraph Create(string serialization) {
		TrackVertex[]? deserializedTracks = JsonSerializer.Deserialize<TrackVertex[]>(serialization);

		if (deserializedTracks == null) {
			throw new ArgumentException($"Serialization {serialization} does not represent TrackGraph correctly.");
		}

		var rootTrack = new TrackVertex(deserializedTracks[0]);
		var tracks = new List<TrackVertex>(deserializedTracks).Slice(1, deserializedTracks.Length).Select(track => new TrackVertex(track)).ToDictionary(track => track.ID, track => track);
		return new TrackGraph(rootTrack: rootTrack, tracks: tracks);

	}
	
	internal TrackVertex GetTrack(TrackId track) {
		if (!_tracks.ContainsKey(track)) {
			throw new ArgumentException($"Track {track} does not exist!");
		}
		
		return _tracks[track];
	}

	#endregion
	
}