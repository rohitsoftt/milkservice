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
using MilkService.API.Domain.Models.Queries.UserQueries;
using MilkService.API.Domain.Models.Queries;

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
        public async Task UpdateProfile(User user)
        {
            var userObj = await _context.User.Where(i => i.Id == user.Id).FirstAsync();
            userObj.FirstName = user.FirstName;
            userObj.LastName = user.LastName;
            userObj.Email = user.Email;
            userObj.MobileNo = user.MobileNo;
            userObj.Address = user.Address;
            userObj.Pincode = user.Pincode;
        }
        public async Task UpdatePassword(int id, string password)
        {
            var userObj = await _context.User.Where(i => i.Id == id).FirstAsync();
            userObj.Password = password;
        }

        public async Task<QueryResult<User>> CustomerListAsync(CustomerUserQuery customerUserQuery)
        {
            IQueryable<User> queryable = _context.User
                                                    .AsNoTracking();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            if (customerUserQuery.UserId.HasValue && customerUserQuery.UserId > 0)
            {
                queryable = queryable.Where(p => p.Id == customerUserQuery.UserId);
            }
            int i = (int)UserRoles.Customer;
            queryable = queryable.Where(p => p.UserRole == i.ToString());
            // Here I count all items present in the database for the given query, to return as part of the pagination data.
            int totalItems = await queryable.CountAsync();

            // Here I apply a simple calculation to skip a given number of items, according to the current page and amount of items per page,
            // and them I return only the amount of desired items. The methods "Skip" and "Take" do the trick here.
            List<User> users = await queryable.Skip((customerUserQuery.Page - 1) * customerUserQuery.ItemsPerPage)
                                                    .Take(customerUserQuery.ItemsPerPage)
                                                    .ToListAsync();

            // Finally I return a query result, containing all items and the amount of items in the database (necessary for client-side calculations ).
            return new QueryResult<User>
            {
                Items = users,
                TotalItems = totalItems,
            };
        }

    }
}
