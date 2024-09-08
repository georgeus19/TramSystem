using System.Net;
using Microsoft.JSInterop;

namespace Frontend.Services.Mission; 

public class MissionPlanningService {
	private readonly HttpClient _httpClient;
	private readonly ErrorHandler _errorHandler;
	private readonly IConfiguration _configuration;

	public MissionPlanningService(HttpClient httpClient, ErrorHandler errorHandler, IConfiguration configuration) {
		_httpClient = httpClient;
		_errorHandler = errorHandler;
		_configuration = configuration;
	}

	public async Task<IEnumerable<MissionPlanning.Api.Mission>?> GetMissions() {
		var response =  await _httpClient.GetAsync($"{GetMissionPlanningServiceUrl()}/api/missions");
		if (await _errorHandler.AlertIfNotMatching(response, new[] { HttpStatusCode.OK })) {
			return null;
		}
		IEnumerable<MissionDto>? missions =  await response.Content.ReadFromJsonAsync<IEnumerable<MissionDto>>();
		return missions?.Select(m => m.ToMission());
	}

	public async Task<MissionPlanning.Api.Mission?> PlanMission() {
		var response = await _httpClient.PostAsync($"{GetMissionPlanningServiceUrl()}/api/missions", null);
		if (await _errorHandler.AlertIfNotMatching(response, new[] { HttpStatusCode.OK })) {
			return null;
		}
		var mission = await response.Content.ReadFromJsonAsync<MissionDto>();
		return mission?.ToMission();
	}
	
	private string GetMissionPlanningServiceUrl() {
		string? serviceUrl = _configuration.GetSection("Services").GetSection("MissionPlanningServiceUrl").Value;
		if (string.IsNullOrEmpty(serviceUrl)) {
			throw new Exception("Mission Planning Service URL not set.");
		}

		return serviceUrl;
	}
}