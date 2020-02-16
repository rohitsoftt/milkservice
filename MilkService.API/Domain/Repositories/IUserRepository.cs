using MilkService.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}
