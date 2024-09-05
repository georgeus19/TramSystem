namespace TrackTramControl.Api; 

/// <summary>
/// Implementation of ids for tracks. It is meant to hide the way the ids are generated and their type.
/// </summary>
public struct TrackId {
	private readonly string _id;
	
	private TrackId(string id) {
		_id = id;
	}
	
	public static TrackId From(string id) => new TrackId(id);

	public override int GetHashCode() {
		return _id.GetHashCode();
	}

	public override string ToString() {
		return _id;
	}

	public static TrackId NewId() {
		return new TrackId($"Track-{Guid.NewGuid().ToString()}");
	}
}