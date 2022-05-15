using System.Collections.Generic;

namespace Pulse.Tracks.Core.Helpers.Extensions
{
    public static class ExtensionChaker
    {

        private static readonly List<string> coverAllowedExtension = new() {
            ".png", ".jpg", ".jpeg"
        };

        public static bool IsCorrectCoverExtension(string extension)
        {
            return coverAllowedExtension.Contains(extension);
        }
    }
}
