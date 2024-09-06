using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace Utils; 

/// <summary>
/// Implementation of ids for trams. It is meant to hide the way the ids are generated and their type.
/// </summary>
public struct TramId : IEquatable<TramId> {
	private readonly string _id;
	
	private TramId(string id) {
		_id = id;
	}

	public static TramId From(string id) => new TramId(id);

	public override int GetHashCode() {
		return _id.GetHashCode();
	}

	public bool Equals(TramId other) {
		return _id == other._id;
	}

	public override bool Equals(object? obj) {
		return _id.Equals(obj);
	}

	public override string ToString() {
		return _id;
	}

	public static TramId NewId() {
		return new TramId($"Tram-{Guid.NewGuid().ToString()}");
	}
}