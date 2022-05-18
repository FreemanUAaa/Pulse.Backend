using Microsoft.Extensions.Logging;
using Pulse.MusicTypes.Core.Database;
using Pulse.MusicTypes.Tests.Databases;
using Moq;

namespace Pulse.MusicTypes.Tests.Tests.Base
{
    public abstract class BaseCommandTests<TLogger> where TLogger : class
    {
        public readonly IDatabaseContext Database;

        public readonly ILogger<TLogger> Logger;

        public BaseCommandTests()
        {
            Database = DatabaseContextFactory.Create();

            Logger = new Mock<ILogger<TLogger>>().Object;
        }
    }
}
