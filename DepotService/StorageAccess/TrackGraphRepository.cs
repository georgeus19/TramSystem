using TrackTramControl.Api;

namespace DepotService.StorageAccess; 

/// <summary>
/// Interface for persisting depo track graph. It is meant to be retrieved and saved whole (e.g. key-value/blob storage). 
/// </summary>
public interface TrackGraphRepository {
	public Task<ModifiableTrackGraph> Get();
	public Task<ModifiableTrackGraph> Update(SerializableTrackGraph trackGraph);
}