using MilkService.API.Domain.Models;
using MilkService.API.Domain.Models.Queries.Response.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Resources.UserResource
{
    public class LoginUserResource
    {
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "Invalid User Role, Value for {0} must be between {1} and {2}.")]
        public string UserRole { get; set; }
    }
}
