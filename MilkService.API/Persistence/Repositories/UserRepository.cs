using MilkService.API.Domain.DBExceptions;
using MilkService.API.Domain.Models;
using MilkService.API.Domain.Repositories;
using MilkService.API.Helpers;
using MilkService.API.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(MilkServiceContext context) : base(context) { }
        public async Task AddAsync(User user)
        {
            user.Password = user.Password.GetHashed();
            await _context.User.AddAsync(user);
        }

        private Exception DublicateEntryException(string v)
        {
            throw new NotImplementedException();
        }
    }
}
