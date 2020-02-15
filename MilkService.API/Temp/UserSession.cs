using System;
using System.Collections.Generic;

namespace MilkService.API.Temp
{
    public partial class UserSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual User User { get; set; }
    }
}
