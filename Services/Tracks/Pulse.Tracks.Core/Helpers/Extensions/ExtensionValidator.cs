namespace Pulse.Tracks.Core.Helpers.Extensions
{
    public static class ExtensionValidator
    {

        public static readonly List<string> TrackCoverAllowedExtension = new() {
            ".png", ".jpg", ".jpeg"
        };

        public static readonly List<string> TrackSongAllowedExtension = new() { 
            ".mp3", ".waw",
        };

        public static bool ValidateCoverExtension(string extension) =>
            TrackCoverAllowedExtension.Contains(extension);

        public static bool ValidateSongExtension(string extension) =>
            TrackSongAllowedExtension.Contains(extension);
    }
}
