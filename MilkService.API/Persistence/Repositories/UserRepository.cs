using Microsoft.EntityFrameworkCore;
using MilkService.API.Domain.DBExceptions;
using MilkService.API.Domain.Models;
using MilkService.API.Domain.Models.Queries.Response.User;
using MilkService.API.Domain.Repositories;
using MilkService.API.Domain.Services.Communication;
using MilkService.API.Helpers;
using MilkService.API.Persistence.Contexts;
using MilkService.API.Resources.UserResource;
using MilkService.API.Domain.Models.DBModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

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

        public async Task<User> LoginAsync(LoginUserResource loginUserResource)
        {
            loginUserResource.Password = loginUserResource.Password.GetHashed();
            var user = await _context.User
                .FirstOrDefaultAsync(user => user.Email == loginUserResource.Email && user.Password == loginUserResource.Password && user.UserRole == loginUserResource.UserRole);
            return user;
        }
        public async Task CreateSession(int id, string token)
        {
            var userSession = await _context.UserSession.Where(i => i.UserId == id).FirstOrDefaultAsync();
            if (userSession!=null)
            {
                userSession.Token = token;
                userSession.LastUpdated = DateTime.Now;
                _context.UserSession.Update(userSession);
            }
            else
            {
                var userSess = new UserSession {
                    UserId = id,
                    LastUpdated = DateTime.Now,
                    Token = token
                };
                await _context.UserSession.AddAsync(userSess);
            }
        }
        public async Task ExtendToken(int id)
        {
            var userSession = await _context.UserSession.Where(i => i.UserId == id).FirstOrDefaultAsync();
            userSession.LastUpdated = DateTime.Now;
            _context.UserSession.Update(userSession);
            await _context.SaveChangesAsync();
        }
        private Exception DublicateEntryException(string v)
        {
            throw new NotImplementedException();
        }
    }
}
