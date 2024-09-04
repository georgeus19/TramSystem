namespace TrackTramControl; 

public interface ReadableTrackGraph : SerializableTrackGraph {
	public ReadableTrackVertex GetRootTrack();
}