using Microsoft.Extensions.Logging;
using Moq;
using Pulse.MusicTypes.Core.Database;
using Pulse.MusicTypes.Tests.Databases;

namespace Pulse.MusicTypes.Tests.Tests.Base
{
    public abstract class BaseQueryTests<TLogger> where TLogger : class
    {
        public readonly IDatabaseContext Database;

        public readonly ILogger<TLogger> Logger;

        public BaseQueryTests()
        {
            Database = DatabaseContextFactory.Create();

            Logger = new Mock<ILogger<TLogger>>().Object;
        }
    }
}
