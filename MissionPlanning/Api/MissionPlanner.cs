using MissionPlanning.Api.TrackGraphFinders;
using TrackTramControl.Api;
using Utils;

namespace MissionPlanning.Api; 

public class MissionPlanner {
	/// <summary>
	/// Plan a mission to the most suitable graph selected by `missionTrackGraphFinder` on `trackGraph`.
	/// The returned mission has assigned tram and given new mission id.
	///
	/// In the future, we expect another parameter that would consist of mission details - mission without id and tram id.
	/// Currently, mission class has no other properties.
	/// </summary>
	public Mission? PlanMission(ReadableTrackGraph trackGraph, MissionTrackGraphFinder missionTrackGraphFinder) {
		TramId? foundTram = missionTrackGraphFinder.FindFreeTram(trackGraph);
		if (foundTram != null) {
			return new Mission(id: MissionId.NewId(), tramId: (TramId)foundTram);	
		} else {
			return null;
		}
	}
}