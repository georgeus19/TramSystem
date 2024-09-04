namespace TrackTramControl; 


/// <summary>
/// Represents the position of a tram on a track. Ideally, it would be an interface or abstract class with
/// specialization for placing trams based on different requirements.
/// 
/// For simplicity, there is just one struct with simple index based implementation.
/// </summary>
public struct TramPosition {
	public TrackId TrackID { get; }
	public int Position { get; }

	public TramPosition(TrackId track, int position) {
		TrackID = track;
		Position = position;
	}
}
