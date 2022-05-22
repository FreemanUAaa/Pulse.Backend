namespace Pulse.MusicTypes.Application.ViewModels
{
    public class ErrorResponseVm
    {
        public string Message { get; set; } = string.Empty;

        public ErrorResponseVm(string message) => Message = message;

        public override string ToString()
        {
            return $"{{\"error\": \"{Message}\" }}";
        }
    }
}
