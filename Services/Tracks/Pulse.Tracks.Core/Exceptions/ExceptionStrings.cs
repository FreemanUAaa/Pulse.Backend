namespace Pulse.Tracks.Core.Exceptions
{
    public static class ExceptionStrings
    {
        public static string NotFound => "Path not found";

        public static string UserNotFound => "User does not exist";

        public static string MusicTypeNotFound => "Music type does not exist";

        public static string EmailIsBusy => "This email is already used";

        public static string FailedToLogin => "The password or the email are no correct";

        public static string EntityAlreadyExists => "entity already exists";

        public static string FailedDeleteFile => "Failed to delete file"; 

        public static string FailedSaveFile => "Failed to create file";

        public static string ExtensionNotSupported => "extension not supported";

        public static string ErrorOccurred => "An unexpected error occurred";
    }
}
