using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Pulse.Users.Application.Mapper;
using Pulse.Users.Application.Services;
using Pulse.Users.Core.Database;
using Pulse.Users.Core.Interfaces.Mapper;
using Pulse.Users.Core.Options;
using Pulse.Users.Core.Services;
using Pulse.Users.Database;
using Pulse.Users.Tests.Helpers.Databases;

namespace Pulse.Users.Tests.Tests.Base
{
    public abstract class BaseQueryTests<TLogger> where TLogger : class
    {
        public readonly IDatabaseContext Database;

        public readonly IFileManager FileManager;

        public readonly ILogger<TLogger> Logger;

        public readonly IMapper Mapper;

        public readonly IOptions<PathOptions> Path;

        public BaseQueryTests()
        {
            Database = DatabaseContextFactory.Create();

            FileManager = new FileManager();

            MapperConfiguration configurationProvider = new(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(typeof(DatabaseContext).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(IMapWith<>).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(AssemblyMappingProfile).Assembly));
            });

            Mapper = configurationProvider.CreateMapper();

            Logger = new Mock<ILogger<TLogger>>().Object;

            Path = Options.Create(new PathOptions()
            {
                Cover = @"D:\sharp\Pulse\Pulse\Services\Users\Pulse.Users.Tests\SavedPhoto\",
            });
        }
    }
}
