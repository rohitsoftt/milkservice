using MilkService.API.Domain.Models.Queries.Response.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Resources.UserResource
{
    public class RegisterUserResource
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MaxLength(13)]
        public string MobileNo { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(6)]
        public string PINCode { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "Invalid User Role, Value for {0} must be between {1} and {2}.")]
        [EnumDataType(typeof(UserRoles))]
        public string UserRole { get; set; }

    }
}
