using System.Net;

namespace Frontend.Services.Depot;

public class DepotService {
	private readonly HttpClient _httpClient;
	private readonly ErrorHandler _errorHandler;
	private readonly IConfiguration _configuration;

	public DepotService(HttpClient httpClient, ErrorHandler errorHandler, IConfiguration configuration) {
		_httpClient = httpClient;
		_errorHandler = errorHandler;
		_configuration = configuration;
	}

	public async Task<Depot?> GetDepot() {
		var response =  await _httpClient.GetAsync($"{GetDepotServiceUrl()}/api/depot");
		if (await _errorHandler.AlertIfNotMatching(response, new[] { HttpStatusCode.OK })) {
			return null;
		}
		DepotDto? depot =  await response.Content.ReadFromJsonAsync<DepotDto>();
		return depot?.ToDepot();
	}

	public async Task<Depot?> AddTram(TramPositionDto position) {
		var response = await _httpClient.PostAsJsonAsync($"{GetDepotServiceUrl()}/api/depot/trams", position);
		if (await _errorHandler.AlertIfNotMatching(response, new[] { HttpStatusCode.OK })) {
			return null;
		}
		var depot = await response.Content.ReadFromJsonAsync<DepotDto>();
		return depot?.ToDepot();
	}
	
	private string GetDepotServiceUrl() {
		string? serviceUrl = _configuration.GetSection("Services").GetSection("DepotServiceUrl").Value;
		if (string.IsNullOrEmpty(serviceUrl)) {
			throw new Exception("Depot Service URL not set.");
		}

		return serviceUrl;
	}
}



