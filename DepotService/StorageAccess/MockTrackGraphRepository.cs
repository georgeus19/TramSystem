using TrackTramControl.Api;
using TrackTramControl.Api.Factory;
using Utils;

namespace DepotService.StorageAccess; 

/// <summary>
/// Mock repository for track graphs which only returns a non-updatable track graph.
/// </summary>
public class MockTrackGraphRepository : TrackGraphRepository {
	public Task<ModifiableTrackGraph> Get() {
		ModifiableTrackGraph trackGraph = TrackGraphFactory.CreateModifiableTrackGraph();
		var rootTrack = trackGraph.GetRootTrack();
		trackGraph.AddTram(TramId.NewId(), new TramPosition(rootTrack.ID, 0));
		trackGraph.AddTram(TramId.NewId(), new TramPosition(rootTrack.ID, 1));
		trackGraph.AddTram(TramId.NewId(), new TramPosition(rootTrack.ID, 2));
		trackGraph.AddTram(TramId.NewId(), new TramPosition(rootTrack.ID, 3));
		trackGraph.AddTram(TramId.NewId(), new TramPosition(rootTrack.ID, 4));
		return Task.FromResult(trackGraph);
	}

	public Task<ModifiableTrackGraph> Update(SerializableTrackGraph trackGraph) {
		return Get();
	}
}