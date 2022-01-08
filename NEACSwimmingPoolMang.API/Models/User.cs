using System;
using System.Collections.Generic;

#nullable disable

namespace NEACSwimmingPoolMang.API.Models
{
    public partial class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
