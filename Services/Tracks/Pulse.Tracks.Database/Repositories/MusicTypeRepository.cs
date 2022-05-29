using Pulse.Tracks.Core.Database;
using Pulse.Tracks.Core.DTOs;
using Pulse.Tracks.Core.Models;
using Pulse.Tracks.Core.Repositories;
using Pulse.Tracks.Database.Repositories.Base;
using Pulse.Tracks.Grpc.Client.Services.Interfaces;

namespace Pulse.Tracks.Database.Repositories
{
    public class MusicTypeRepository : BaseRepository<MusicType>, IMusicTypeRepository
    {
        private readonly IMusicTypesGrpcService musicTypesGrpcService;

        public MusicTypeRepository(IDatabaseContext database, IMusicTypesGrpcService musicTypesGrpcService) : base(database) => 
            this.musicTypesGrpcService = musicTypesGrpcService;

        public async Task<IEnumerable<MusicTypeIsExistsDTO>> IsExists(IEnumerable<Guid> musictTypeIds) =>
            await musicTypesGrpcService.IsExists(musictTypeIds);   
    }
}
