namespace MissionPlanning.Api; 

public interface MissionRepository {
	public Task<IEnumerable<Mission>> GetMissions();
	
	public Task<Mission> Create(Mission mission);
}