using Pulse.MusicTypes.Core.Models;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Queries.GetMusicTypeList
{
    public class GetMusicTypeListQueryVm
    {
        public List<MusicType> MusicTypes { get; set; } = new();
    }
}
