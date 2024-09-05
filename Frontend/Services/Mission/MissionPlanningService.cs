namespace Frontend.Services.Mission; 

public class MissionPlanningService {
	private readonly HttpClient _httpClient;

	public MissionPlanningService(HttpClient httpClient) {
		_httpClient = httpClient;
	}

	public async Task<IEnumerable<MissionPlanning.Api.Mission>?> GetMissions() {
		IEnumerable<MissionDto>? missions =  await _httpClient.GetFromJsonAsync<IEnumerable<MissionDto>>("http://localhost:5052/api/missions");
		return missions?.Select(m => m.ToMission());
	}

	public async Task<MissionPlanning.Api.Mission?> PlanMission() {
		var response = await _httpClient.PostAsync("http://localhost:5052/api/missions", null);
		var mission = await response.Content.ReadFromJsonAsync<MissionDto>();
		return mission?.ToMission();
	}
}