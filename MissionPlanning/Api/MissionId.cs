namespace MissionPlanning.Api; 

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