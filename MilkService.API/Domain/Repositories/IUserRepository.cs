﻿using MilkService.API.Domain.Models;
using MilkService.API.Domain.Services.Communication;
using MilkService.API.Resources.UserResource;
using MilkService.API.Domain.Models.DBModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> LoginAsync(LoginUserResource loginUserResource);
        Task CreateSession(int Id, string token);
    }
}
