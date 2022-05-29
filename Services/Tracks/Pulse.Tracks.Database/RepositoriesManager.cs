using Pulse.Tracks.Core.Database;
using Pulse.Tracks.Core.Repositories;
using Pulse.Tracks.Database.Repositories;
using Pulse.Tracks.Grpc.Client.Services;
using Pulse.Tracks.Grpc.Client.Services.Interfaces;

namespace Pulse.Tracks.Database
{
    public class RepositoriesManager : IRepositoriesManager
    {
        public IMusicTypeRepository MusicType { get; }

        public ITrackCoverRepository TrackCover { get; }

        public ITrackRepository Track { get; }

        public ITrackSongRepository TrackSong { get; }

        public IUserRepository User { get; }

        private readonly IDatabaseContext database;

        public RepositoriesManager(IDatabaseContext database, IMusicTypesGrpcService musicTypesGrpcService, UsersGrpcService usersGrpcService) =>
            (MusicType, TrackCover, Track, TrackSong, User, this.database) = 
                (
                    new MusicTypeRepository(database, musicTypesGrpcService), 
                    new TrackCoverRepository(database), 
                    new TrackRepository(database), 
                    new TrackSongRepository(database), 
                    new UserRepository(usersGrpcService),
                    database
            );

        public async Task Save() => await database.SaveChangesAsync();
    }
}
