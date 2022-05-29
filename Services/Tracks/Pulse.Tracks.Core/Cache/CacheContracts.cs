namespace Pulse.Tracks.Core.Cache
{
    public static class CacheContracts
    {
        public static string GetTrackDetailsKey(Guid trackId) => $"track-details:{trackId}";
    }
}
