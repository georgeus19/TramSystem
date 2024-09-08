using System.Text.Json;
using MissionPlanning.Api;
using StackExchange.Redis;
using Utils;

namespace MissionPlanningService.StorageAccess; 

/// <summary>
/// Very simple `MissionRepository` implementation storing all missions just as Redis list.
/// </summary>
public class RedisMissionRepository : MissionRepository {
	private readonly IDatabase _db;
	const string MissionsKey = "missions";
	
	public RedisMissionRepository(IConfiguration configuration) {
		string? connString = configuration.GetSection("Storage").GetSection("RedisConnectionString").Value;
		
		var redis = ConnectionMultiplexer.Connect(connString ?? string.Empty);
		_db = redis.GetDatabase();
	}
	
	public async Task<IEnumerable<Mission>> GetMissions() {
		var list = await _db.ListRangeAsync(MissionsKey);
		IEnumerable<Mission> missions = list.Where(v => v.HasValue)
			.Select(v => JsonSerializer.Deserialize<StoredMission>(v!))
			.Select(m => new Mission(id: MissionId.From(m!.ID!), tramId: TramId.From(m.TramID!)));
		return missions;
	}

	public async Task<Mission> Create(Mission mission) {
		await _db.ListLeftPushAsync(MissionsKey, JsonSerializer.Serialize(new StoredMission(){ID=mission.ID.ToString(), TramID = mission.TramID.ToString()}));
		return mission;
	}

	private class StoredMission {
		public string? ID { get; set; }
		public string? TramID { get; set; }
	}
}
