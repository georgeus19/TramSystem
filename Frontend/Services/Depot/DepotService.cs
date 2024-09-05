namespace Frontend.Services.Depot;

public class DepotService {
	private readonly HttpClient _httpClient;

	public DepotService(HttpClient httpClient) {
		_httpClient = httpClient;
	}

	public async Task<Depot?> GetDepot() {
		DepotDto? depot =  await _httpClient.GetFromJsonAsync<DepotDto>("http://localhost:5052/api/depot");
		return depot?.ToDepot();
	}

	public async Task<Depot?> AddTram(TramPositionDto position) {
		var response = await _httpClient.PostAsJsonAsync<TramPositionDto>("http://localhost:5052/api/depot/trams", position);
		var depot = await response.Content.ReadFromJsonAsync<DepotDto>();
		return depot?.ToDepot();
	}
}



