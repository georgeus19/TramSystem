namespace MissionPlanningService.Controllers.Dto; 

/// <summary>
/// Data Transfer Object for transmitting depot data over network.
/// </summary>
public class DepotDto {
	public string TrackGraph { get; set; }

	public DepotDto(string trackGraph) {
		TrackGraph = trackGraph;
	}

	public DepotDto() {
		TrackGraph = "X";
	}
}