using MediatR;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Queries.GetIsExistsMusicType
{
    public class GetIsExistsMusicTypeQuery : IRequest<GetIsExistsMusicTypeQueryVm>
    {
        public List<Guid> MusicTypeIds { get; set; } = new();
    }
}
