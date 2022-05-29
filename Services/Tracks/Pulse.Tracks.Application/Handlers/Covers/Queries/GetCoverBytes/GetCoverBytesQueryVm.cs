namespace Pulse.Tracks.Application.Handlers.Covers.Queries.GetCoverBytes
{
    public class GetCoverBytesQueryVm
    {
        public byte[] Bytes { get; set; } = Array.Empty<byte>();

        public string Extension { get; set; } = string.Empty;
    }
}
