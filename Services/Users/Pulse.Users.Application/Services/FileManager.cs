using Microsoft.AspNetCore.Http;
using Pulse.Users.Core.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Pulse.Users.Application.Services
{
    public class FileManager : IFileManager
    {
        public async Task DeleteFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path can not be null or empty");
            }

            await Task.Run(() => File.Delete(path));
        }

        public async Task SaveFile(IFormFile file, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path can not be null or empty");
            }

            if (file == null)
            {
                throw new ArgumentException("File can not be null");
            }

            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}
