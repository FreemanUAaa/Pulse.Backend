using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Pulse.Users.Application.Services;
using Pulse.Users.Core.Database;
using Pulse.Users.Core.Options;
using Pulse.Users.Core.Services;
using Pulse.Users.Producers.Interfaces;
using Pulse.Users.Tests.Helpers.Databases;

namespace Pulse.Users.Tests.Tests.Base
{
    public abstract class BaseCommandTests<TLogger> where TLogger : class
    {
        public readonly IDatabaseContext Database;

        public readonly IFileManager FileManager;

        public readonly IDistributedCache Cache;

        public readonly ILogger<TLogger> Logger;

        public readonly IOptions<PathOptions> Path;

        public readonly IUserProducer UserProducer;

        public BaseCommandTests()
        {
            Database = DatabaseContextFactory.Create();

            FileManager = new FileManager();

            Cache = new Mock<IDistributedCache>().Object;

            Logger = new Mock<ILogger<TLogger>>().Object;

            UserProducer = new Mock<IUserProducer>().Object;

            Path = Options.Create(new PathOptions()
            {
                Cover = @"D:\sharp\Pulse\Pulse\Services\Users\Pulse.Users.Tests\SavedPhoto\",
            });
        }
    }
}
