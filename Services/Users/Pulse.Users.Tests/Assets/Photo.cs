using System.IO;

namespace Pulse.Users.Tests.Assets
{
    public static class Photo
    {
        public static byte[] Bytes { get; set; }

        static Photo()
        {
            Bytes = File.ReadAllBytes(@"D:\sharp\Pulse\Pulse\Services\Users\Pulse.Users.Tests\Assets\img.png");
        }
    }
}
