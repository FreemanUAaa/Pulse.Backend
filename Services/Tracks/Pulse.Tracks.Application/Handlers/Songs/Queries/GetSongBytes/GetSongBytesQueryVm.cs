namespace Pulse.Tracks.Application.Handlers.Songs.Queries.GetSongBytes
{
    public class GetSongBytesQueryVm
    {
        public byte[] Bytes { get; set; } = Array.Empty<byte>();

        public string Extension { get; set; } = string.Empty;
    }
}
