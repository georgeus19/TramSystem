using MissionPlanning.Api;

namespace MissionPlanningService.Controllers.Dto; 

/// <summary>
/// Data Transfer Object for transmitting mission data over network.
/// </summary>
public class MissionDto {
	public string ID { get; set; }
	public string TramID { get; set; }

	public MissionDto() {
		ID = "X";
		TramID = "XX";
	}
	
	public MissionDto(Mission mission) {
		ID = mission.ID.ToString();
		TramID = mission.TramID.ToString();
	}
}