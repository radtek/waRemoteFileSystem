﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waRemoteFileSystem.Models
{
    public class RegisterUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
    }
}
