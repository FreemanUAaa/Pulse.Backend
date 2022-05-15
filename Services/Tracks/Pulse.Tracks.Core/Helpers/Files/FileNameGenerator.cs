namespace Pulse.Tracks.Core.Helpers.Files
{
    public static class FileNameGenerator
    {
        public static string GenerateUniqueFileName(string path, string fileExtension, int attempts)
        {
            for (int i = 0; i <= attempts; i++)
            {
                string newFileName = GetRandomFileName(fileExtension);

                string newFilePath = Path.Combine(path, newFileName);

                if (!File.Exists(newFilePath))
                {
                    return newFileName;
                }
            }

            throw new Exception("Can't generate new file name. Increase attempts");
        }

        public static string GetRandomFileName(string fileExtension)
        {
            if (fileExtension.Contains('.'))
            {
                return Path.GetRandomFileName() + fileExtension;
            }

            return Path.GetRandomFileName() + "." + fileExtension;
        }
    }
}
