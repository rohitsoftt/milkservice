using MilkService.API.Domain.Models;
using MilkService.API.Domain.Services.Communication;
using MilkService.API.Domain.Services.Communication.Response;
using MilkService.API.Resources.UserResource;
using MilkService.API.Domain.Models.DBModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Domain.Services
{
    public interface IUserService
    {
        Task<OResponse> SaveAsync(User user);
        Task<UserLoginResponse> LoginAsync(LoginUserResource loginUserResource);
    }
}
