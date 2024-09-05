using MissionPlanning.Api;
using Utils;

namespace Frontend.Services.Mission; 

public class MissionDto {
	public string ID { get; set; }
	public string TramID { get; set; }

	public MissionDto() {
		ID = "X";
		TramID = "XX";
	}
	
	public MissionDto(MissionPlanning.Api.Mission mission) {
		ID = mission.ID.ToString();
		TramID = mission.TramID.ToString();
	}

	public MissionPlanning.Api.Mission ToMission() {
		return new MissionPlanning.Api.Mission(id: MissionId.From(ID), tramId: TramId.From(TramID));
	}
}