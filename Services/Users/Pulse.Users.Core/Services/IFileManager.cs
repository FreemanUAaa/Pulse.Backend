using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Pulse.Users.Core.Services
{
    public interface IFileManager
    {
        Task SaveFile(IFormFile file, string path);

        Task DeleteFile(string path);
    }
}
