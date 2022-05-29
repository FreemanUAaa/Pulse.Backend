using Microsoft.AspNetCore.Http;
using Pulse.Tracks.Core.Exceptions;

namespace Pulse.Tracks.Core.Helpers.Files
{
    public static class FileManager
    {
        public static async Task<string> SaveFile(IFormFile file, string path, Func<string, bool> extensionValidator)
        {
            string extension = Path.GetExtension(file.FileName);

            if (!extensionValidator(extension))
            {
                throw new Exception(ExceptionStrings.ExtensionNotSupported);
            }

            string newFileName;

            try
            {
                newFileName = FileNameGenerator.GenerateUniqueFileName(path, extension, 10);
            }
            catch
            {
                throw new Exception(ExceptionStrings.FailedSaveFile);
            }


            string filePath = Path.Combine(path, newFileName);

            try
            {
                using FileStream stream = new(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
            }
            catch
            {
                throw new Exception(ExceptionStrings.FailedSaveFile);
            }

            return newFileName;
        }

        public static async Task DeleteFile(string path)
        {
            if (!File.Exists(path))
            {
                await Task.Run(() => File.Delete(path));
            }
        }
    }
}
