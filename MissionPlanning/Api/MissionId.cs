namespace MissionPlanning.Api; 
/// <summary>
/// Implementation of ids for missions. It is meant to hide the way the ids are generated and their type.
/// </summary>
public class MissionId {
	private readonly string _id;
	
	private MissionId(string id) {
		_id = id;
	}

	public static MissionId From(string id) => new MissionId(id);

	public override int GetHashCode() {
		return _id.GetHashCode();
	}

	public override bool Equals(object? obj) {
		return _id.Equals(obj);
	}

	public override string ToString() {
		return _id;
	}

	public static MissionId NewId() {
		return new MissionId($"Mission-{Guid.NewGuid().ToString()}");
	}
}