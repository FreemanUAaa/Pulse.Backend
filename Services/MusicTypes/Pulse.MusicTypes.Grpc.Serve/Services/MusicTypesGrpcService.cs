using Grpc.Core;
using MediatR;
using Pulse.MusicTypes.Application.Handlers.MusicTypes.Queries.GetIsExistsMusicType;

namespace Pulse.MusicTypes.Grpc.Serve.Services
{
    public class MusicTypesGrpcService : GrpcMusicTypes.GrpcMusicTypesBase
    {
        private readonly ILogger<MusicTypesGrpcService> logger;

        private readonly IMediator mediator;

        public MusicTypesGrpcService(IMediator mediator, ILogger<MusicTypesGrpcService> logger) =>
            (this.mediator, this.logger) = (mediator, logger);

        public override async Task<MusicTypeIsExistsResponse> IsExists(MusicTypeIsExistsRequest request, ServerCallContext context)
        {
            List<Guid> musicTypeIds = request.MusicTypeIds.Select(x => Guid.Parse(x)).ToList();

            GetIsExistsMusicTypeQuery query = new() { MusicTypeIds = musicTypeIds };

            GetIsExistsMusicTypeQueryVm vm = await mediator.Send(query);

            List<MusicTypeIsExists> items = new();

            foreach (var item in vm.Exists)
            {
                items.Add(new()
                {
                    IsExists = item.IsExists,
                    MusicTypeId = item.MusicTypeId.ToString(),
                });
            }

            MusicTypeIsExistsResponse response = new();

            response.Exists.Add(items);

            logger.LogInformation("Grpc => call is music types exists");

            return response;
        }
    }
}
