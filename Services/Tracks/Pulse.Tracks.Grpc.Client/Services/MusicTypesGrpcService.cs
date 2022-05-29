using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Pulse.MusicTypes.Grpc.Serve;
using Pulse.Tracks.Core.DTOs;
using Pulse.Tracks.Core.Options;
using Pulse.Tracks.Grpc.Client.Services.Interfaces;

namespace Pulse.Tracks.Grpc.Client.Services
{
    public class MusicTypesGrpcService : IMusicTypesGrpcService
    {
        private readonly GrpcOptions grpcOptions;

        public MusicTypesGrpcService(IOptions<GrpcOptions> grpcOptions) => this.grpcOptions = grpcOptions.Value;

        public async Task<IEnumerable<MusicTypeIsExistsDTO>> IsExists(IEnumerable<Guid> musicTypeIds)
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(grpcOptions.MusicTypeConnectionString);
            var client = new GrpcMusicTypes.GrpcMusicTypesClient(channel);

            MusicTypeIsExistsRequest request = new();

            request.MusicTypeIds.Add(musicTypeIds.Select(x => x.ToString()).ToArray());

            MusicTypeIsExistsResponse reply = await client.IsExistsAsync(request);

            List<MusicTypeIsExistsDTO> result = new();

            foreach (MusicTypeIsExists exist in reply.Exists)
            {
                result.Add(new()
                {
                    IsExists = exist.IsExists,
                    MecisTypeId = Guid.Parse(exist.MusicTypeId),
                });
            }

            return result;
        }
    }
}
