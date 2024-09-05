using Utils;

namespace MissionPlanning.Api; 

public class Mission {
	public MissionId ID { get; }
	public TramId TramID { get; }

	public Mission(MissionId id, TramId tramId) {
		ID = id;
		TramID = tramId;
	}
}