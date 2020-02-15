using MilkService.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MilkService.API.Domain.Models.DBModels.UserModels;
using MilkService.API.Domain.Models.Queries.Response.User;

namespace MilkService.API.Domain.Services.Communication
{
    public class UserResponse: BaseResponse<User>
    {
        public UserResponse(User user) : base(user) { }

        public UserResponse(string message) : base(message) { }
    }
    public class UserLoginResponse: BaseResponse<UserLoginDetails>
    {
        public UserLoginResponse(UserLoginDetails user): base(user) { }
        public UserLoginResponse(string message) : base(message) { }
    }

}
