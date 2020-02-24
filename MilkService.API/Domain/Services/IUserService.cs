using MilkService.API.Domain.Models;
using MilkService.API.Domain.Services.Communication;
using MilkService.API.Domain.Services.Communication.Response;
using MilkService.API.Resources.UserResource;
using MilkService.API.Domain.Models.DBModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MilkService.API.Domain.Models.Queries.Response.User;

namespace MilkService.API.Domain.Services
{
    public interface IUserService
    {
        Task<OResponse> SaveAsync(User user);
        Task<ServiceResponse<UserLoginDetails>> LoginAsync(LoginUserResource loginUserResource);
        Task<ServiceResponse<User>> UpdateProfile(User user);
        Task<OResponse> UpdatePasswordAsync(int id, string password, string oldPassword);
        Task<OResponse> AddCustomerUserAsync(User user);
    }
}
