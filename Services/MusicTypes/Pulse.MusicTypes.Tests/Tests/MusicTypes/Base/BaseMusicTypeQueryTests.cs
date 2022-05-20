using Pulse.MusicTypes.Core.Models;
using Pulse.MusicTypes.Tests.Tests.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pulse.MusicTypes.Tests.Tests.MusicTypes.Base
{
    public abstract class BaseMusicTypeQueryTests<TLogger> : BaseCommandTests<TLogger> where TLogger : class
    {
        public MusicType MusicTypeEntity { get; set; }

        public BaseMusicTypeQueryTests()
        {
            MusicTypeEntity = new()
            {
                Id = Guid.NewGuid(),
                Name = "test-name",
            };
        }

        public async Task SaveMusicType(MusicType musicType, CancellationToken cancellationToken = default)
        {
            Database.MusicTypes.Add(musicType);
            await Database.SaveChangesAsync(cancellationToken);
        }
    }
}
