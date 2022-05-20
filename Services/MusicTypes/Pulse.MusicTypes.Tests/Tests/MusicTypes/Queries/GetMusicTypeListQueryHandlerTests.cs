using Pulse.MusicTypes.Application.Handlers.MusicTypes.Queries.GetMusicTypeList;
using Pulse.MusicTypes.Tests.Tests.MusicTypes.Base;
using Shouldly;
using System.Linq;
using System.Threading;
using Xunit;

namespace Pulse.MusicTypes.Tests.Tests.MusicTypes.Queries
{
    public class GetMusicTypeListQueryHandlerTests : BaseMusicTypeQueryTests<GetMusicTypeListQueryHandler>
    {
        [Fact]
        public async void GetMusicTypeListQueryHandlerSuccess()
        {
            await SaveMusicType(MusicTypeEntity);
            GetMusicTypeListQueryHandler handler = new(Database);
            GetMusicTypeListQuery query = new() { MusicTypeIds = new() { MusicTypeEntity.Id } };


            GetMusicTypeListQueryVm vm = await handler.Handle(query, CancellationToken.None);


            vm.MusicTypes.Count.ShouldBe(1);

            Assert.NotNull(vm.MusicTypes.First());
        }
    }
}
