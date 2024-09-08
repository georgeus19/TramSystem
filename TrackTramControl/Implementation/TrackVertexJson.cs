using System.Text.Json.Serialization;
using TrackTramControl.Api;
using Utils;

namespace TrackTramControl.Implementation; 

/// <summary>
/// Class for handling the data of <see cref="TrackVertex"/> that can be transformed to JSON easily. It is used when
/// serialization and subsequent deserialization of TrackVertex is made.
/// </summary>
internal class TrackVertexJson {
	[JsonInclude]
	public string ID;
	[JsonInclude]
	public List<string> Trams;
	[JsonInclude]
	public List<TrackVertexJson> LeftAdjacentTracks;
	[JsonInclude]
	public List<TrackVertexJson> RightAdjacentTracks;

	internal TrackVertexJson(TrackId id, IEnumerable<TramId> trams, IEnumerable<TrackVertexJson> leftAdjacent, IEnumerable<TrackVertexJson> rightAdjacent) {
		ID = id.ToString();
		Trams = new List<string>(trams.Select(tram => tram.ToString()));
		LeftAdjacentTracks = new List<TrackVertexJson>(leftAdjacent);
		RightAdjacentTracks = new List<TrackVertexJson>(rightAdjacent);
	}

	public TrackVertexJson() {
		ID = "X";
		Trams = new List<string>();
		LeftAdjacentTracks = new List<TrackVertexJson>();
		RightAdjacentTracks = new List<TrackVertexJson>();
	}
	
	internal TrackVertexJson(TrackVertexJson other) {
		ID = other.ID;
		Trams = other.Trams;
		LeftAdjacentTracks = other.LeftAdjacentTracks;
		RightAdjacentTracks = other.RightAdjacentTracks;
	}

	internal TrackVertex ToTrackVertex(IDictionary<TrackId, TrackVertex> createdTracks) {
		var track = new TrackVertex(id: TrackId.From(ID), trams: Trams.Select(t => TramId.From(t)), leftAdjacent: LeftAdjacentTracks.Select(t =>  NewTrackVertex(createdTracks, t)), rightAdjacent: RightAdjacentTracks.Select(t => NewTrackVertex(createdTracks, t)));
		createdTracks[track.ID] = track;
		return track;
	}

	private TrackVertex NewTrackVertex(IDictionary<TrackId, TrackVertex> createdTracks, TrackVertexJson t) {
		if (createdTracks.ContainsKey(TrackId.From(t.ID))) {
			return createdTracks[TrackId.From(t.ID)];
		} else {
			return t.ToTrackVertex(createdTracks);
		}
	}
}