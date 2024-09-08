using StackExchange.Redis;
using TrackTramControl.Api;
using TrackTramControl.Api.Factory;

namespace DepotService.StorageAccess; 

/// <summary>
/// Redis implementation of <see cref="TrackGraphRepository"/> that stores the whole graph as one string.
/// It just retrieves or saves the whole thing.
/// </summary>
public class RedisTrackGraphRepository : TrackGraphRepository {
	private readonly IDatabase _db;
	const string DepotKey = "depot";
	
	public RedisTrackGraphRepository(IConfiguration configuration) {
		string? connString = configuration.GetSection("Storage").GetSection("RedisConnectionString").Value;
		var redis = ConnectionMultiplexer.Connect(connString ?? string.Empty);
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