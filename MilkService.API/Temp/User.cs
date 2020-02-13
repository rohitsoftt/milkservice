using System;
using System.Collections.Generic;

namespace MilkService.API.Temp
{
    public partial class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobileno { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
    }
}
