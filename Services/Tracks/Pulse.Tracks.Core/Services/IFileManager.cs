using Microsoft.AspNetCore.Http;

namespace Pulse.Tracks.Core.Services
{
    public interface IFileManager
    {
        Task SaveFile(IFormFile file, string path);

        Task DeleteFile(string path);
    }
}
