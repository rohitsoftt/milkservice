using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MilkService.API.Domain.Models.Queries.Response.User
{
    public enum UserRoles
    {
        [EnumMember(Value = "Admin")]
        Admin = 1,
        [EnumMember(Value = "ServiceProvider")]
        ServiceProvider = 2,
        [EnumMember(Value = "Customer")]
        Customer = 3
    }
    public class UserDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string PINCode { get; set; }
        public string UserRole { get; set; }
    }
    public class UserLoginDetails: UserDetails
    {
        public string Token { get; set; }
    }
}
