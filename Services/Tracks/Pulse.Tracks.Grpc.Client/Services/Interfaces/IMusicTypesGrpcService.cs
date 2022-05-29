using Pulse.Tracks.Core.DTOs;

namespace Pulse.Tracks.Grpc.Client.Services.Interfaces
{
    public interface IMusicTypesGrpcService
    {
        Task<IEnumerable<MusicTypeIsExistsDTO>> IsExists(IEnumerable<Guid> musicTypeIds);
    }
}
