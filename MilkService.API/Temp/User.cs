using System;
using System.Collections.Generic;

namespace MilkService.API.Temp
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string UserRole { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual UserSession UserSession { get; set; }
    }
}
