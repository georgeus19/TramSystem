using System.Text.Json.Serialization;

namespace DepotService.Controllers.Dto; 

public class DepotDto {
	public string TrackGraph { get; set; }

	public DepotDto(string trackGraph) {
		TrackGraph = trackGraph;
	}

	public DepotDto() {
		
	}
}