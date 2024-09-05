using StackExchange.Redis;
using TrackTramControl.Api;
using TrackTramControl.Api.Factory;

namespace DepotService.StorageAccess; 

public class RedisTrackGraphRepository : TrackGraphRepository {
	private IDatabase _db;
	const string DepotKey = "depot";
	
	public RedisTrackGraphRepository() {
		var redis = ConnectionMultiplexer.Connect("localhost");
		_db = redis.GetDatabase();
	}
	public Task<ModifiableTrackGraph> Get() {
		RedisValue value = _db.StringGet(DepotKey);
		if (!value.HasValue) {
			ModifiableTrackGraph trackGraph = TrackGraphFactory.CreateModifiableTrackGraph();
			_db.StringSet(DepotKey, trackGraph.Serialize());
			return Task.FromResult(trackGraph);
		}

		return Task.FromResult(TrackGraphFactory.CreateModifiableTrackGraph(value));
	}

	public Task<ModifiableTrackGraph> Update(SerializableTrackGraph trackGraph) {
		_db.StringSet(DepotKey, trackGraph.Serialize());
		return Get();
	}
}