using MediatR;
using Microsoft.EntityFrameworkCore;
using Pulse.MusicTypes.Core.Database;
using Pulse.MusicTypes.Core.Models;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Queries.GetMusicTypeList
{
    public class GetMusicTypeListQueryHandler : IRequestHandler<GetMusicTypeListQuery, GetMusicTypeListQueryVm>
    {
        private readonly IDatabaseContext database;

        public GetMusicTypeListQueryHandler(IDatabaseContext database) => this.database = database;

        public async Task<GetMusicTypeListQueryVm> Handle(GetMusicTypeListQuery request, CancellationToken cancellationToken)
        {
            List<MusicType> musicTypes = await database.MusicTypes.Where(x => request.MusicTypeIds.Contains(x.Id)).ToListAsync(cancellationToken);

            return new() { MusicTypes = musicTypes };
        }
    }
}
