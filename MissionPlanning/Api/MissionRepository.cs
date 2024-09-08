namespace MissionPlanning.Api; 

/// <summary>
/// Interface for persisting the missions data.
/// </summary>
public interface MissionRepository {
	public Task<IEnumerable<Mission>> GetMissions();
	
	public Task<Mission> Create(Mission mission);
}