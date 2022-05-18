using MediatR;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Queries.GetMusicTypeList
{
    public class GetMusicTypeListQuery : IRequest<GetMusicTypeListQueryVm>
    {
        public List<Guid> MusicTypeIds { get; set; } = new();
    }
}
