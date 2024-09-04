using Utils;

namespace TrackTramControl;

public interface ModifiableTrackGraph {
	public void AddTram(TramId tram, TramPosition position);
	public ReadableTrackVertex AddLeftTrack(TrackId adjacentTrack);
	public ReadableTrackVertex AddRightTrack(TrackId adjacentTrack);
}