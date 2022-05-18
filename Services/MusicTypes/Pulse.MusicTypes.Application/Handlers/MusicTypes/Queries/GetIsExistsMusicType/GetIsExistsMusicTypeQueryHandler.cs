using MediatR;
using Microsoft.EntityFrameworkCore;
using Pulse.MusicTypes.Core.Database;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Queries.GetIsExistsMusicType
{
    public class GetIsExistsMusicTypeQueryHandler : IRequestHandler<GetIsExistsMusicTypeQuery, GetIsExistsMusicTypeQueryVm>
    {
        private readonly IDatabaseContext database;

        public GetIsExistsMusicTypeQueryHandler(IDatabaseContext database) => this.database = database;

        public async Task<GetIsExistsMusicTypeQueryVm> Handle(GetIsExistsMusicTypeQuery request, CancellationToken cancellationToken)
        {
            List<GetIsExistsMusicTypeQueryVmItem> items = new();

            foreach (Guid musicTypeId in request.MusicTypeIds)
            {
                bool isExists = await database.MusicTypes.AnyAsync(x => x.Id == musicTypeId, cancellationToken);

                GetIsExistsMusicTypeQueryVmItem item = new()
                {
                    MusicTypeId = musicTypeId,
                    IsExists = isExists,
                };

                items.Add(item);
            }

            return new() { Exists = items };
        }
    }
}
