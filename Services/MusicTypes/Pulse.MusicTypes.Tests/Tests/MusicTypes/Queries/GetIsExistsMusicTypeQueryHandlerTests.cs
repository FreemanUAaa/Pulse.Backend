using Pulse.MusicTypes.Application.Handlers.MusicTypes.Queries.GetIsExistsMusicType;
using Pulse.MusicTypes.Tests.Tests.MusicTypes.Base;
using System.Linq;
using System.Threading;
using Xunit;

namespace Pulse.MusicTypes.Tests.Tests.MusicTypes.Queries
{
    public class GetIsExistsMusicTypeQueryHandlerTests : BaseMusicTypeQueryTests<GetIsExistsMusicTypeQueryHandler>
    {
        [Fact]
        public async void GetIsExistsMusicTypeQueryHandlerSuccess()
        {
            await SaveMusicType(MusicTypeEntity);
            GetIsExistsMusicTypeQueryHandler handler = new(Database);
            GetIsExistsMusicTypeQuery query = new() { MusicTypeIds = new() { MusicTypeEntity.Id } };


            GetIsExistsMusicTypeQueryVm vm = await handler.Handle(query, CancellationToken.None);


            bool isExists = vm.Exists.Where(x => x.MusicTypeId == MusicTypeEntity.Id).Select(x => x.IsExists).First();

            Assert.True(isExists);
        }
    }
}
