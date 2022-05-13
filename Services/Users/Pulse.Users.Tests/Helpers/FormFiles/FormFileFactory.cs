using Microsoft.AspNetCore.Http;
using System.IO;

namespace Pulse.Users.Tests.Helpers.FormFiles
{
    public static class FormFileFactory
    {
        public static IFormFile Create(byte[] bytes, string fileName)
        {
            MemoryStream stream = new(bytes);

            return new FormFile(stream, 0, bytes.Length, fileName, fileName);
        }
    }
}
