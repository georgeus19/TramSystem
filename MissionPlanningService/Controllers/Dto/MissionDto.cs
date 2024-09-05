using MissionPlanning.Api;

namespace MissionPlanningService.Controllers.Dto; 

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