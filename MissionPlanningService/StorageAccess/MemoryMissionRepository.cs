using MissionPlanning.Api;
using Utils;

namespace MissionPlanningService.StorageAccess; 

/// <summary>
/// Mission repository which does not connect to any persistent storage and instead saves all missions in memory as
/// its private data. If used, it must be registered as a singleton so that mission can persist from request to request.
/// </summary>
public class MemoryMissionRepository : MissionRepository {
	private readonly List<Mission> _missions = new List<Mission>();
	public Task<IEnumerable<Mission>> GetMissions() {
		IEnumerable<Mission> missions = _missions;

		return Task.FromResult(missions);
	}

	public Task<Mission> Create(Mission mission) {
		_missions.Add(mission);
		return Task.FromResult(mission);
	}
}