namespace Pulse.Tracks.Core.Repositories
{
    public interface IRepositoriesManager
    {
        IMusicTypeRepository MusicType { get; }

        ITrackCoverRepository TrackCover { get; }

        ITrackRepository Track { get; }

        ITrackSongRepository TrackSong { get; }

        IUserRepository User { get; }

        Task Save();
    }
}
