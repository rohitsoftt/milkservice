using MilkService.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Domain.Services.Communication
{
    public class UserResponse: BaseResponse<User>
    {
        public UserResponse(User user) : base(user) { }

        public UserResponse(string message) : base(message) { }
    }
}
