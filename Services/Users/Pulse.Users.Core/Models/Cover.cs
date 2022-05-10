using System;

namespace Pulse.Users.Core.Models
{
    public class Cover
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FileName { get; set; }
    }
}
