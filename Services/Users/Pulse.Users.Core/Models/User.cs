﻿using System;
using System.Collections.Generic;

namespace Pulse.Users.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; } 

        public string Email { get; set; }

        public string Location { get; set; }

        public string Website { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public List<MusicType> MusicTypes { get; set; }
    }
}
