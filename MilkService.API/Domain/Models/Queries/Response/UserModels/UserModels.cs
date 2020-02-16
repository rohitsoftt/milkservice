using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MilkService.API.Domain.Models.Queries.Response.User
{
    public enum UserRoles
    {
        None = 0,
        [EnumMember(Value = "Admin")]
        Admin = 1,
        [EnumMember(Value = "ServiceProvider")]
        ServiceProvider = 2,
        [EnumMember(Value = "Customer")]
        Customer = 3
    }
    public interface IUserDetails
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string MobileNo { get; set; }
        string Address { get; set; }
        string PINCode { get; set; }
        string UserRole { get; set; }
    }
    public class UserDetails: IUserDetails
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
