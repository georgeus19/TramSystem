namespace TrackTramControl; 

public struct TrackId {
	private readonly string _id;
	
	private TrackId(string id) {
		_id = id;
	}

	public override int GetHashCode() {
		return _id.GetHashCode();
	}

	public static TrackId NewId() {
		return new TrackId(Guid.NewGuid().ToString());
	}
}