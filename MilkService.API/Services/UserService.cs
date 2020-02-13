using Microsoft.EntityFrameworkCore;
using MilkService.API.Domain.Models;
using MilkService.API.Domain.Repositories;
using MilkService.API.Domain.Services;
using MilkService.API.Domain.Services.Communication.Response;
using MilkService.API.Persistence.Contexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MilkServiceContext _context;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, MilkServiceContext context)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<OResponse> SaveAsync(User user)
        {
            try
            {
                /*
                 * Checking Email or Mobile number is already exist or not in database
                 * */
                if (await _context.User.Where(i => i.Email == user.Email || i.MobileNo == user.MobileNo).CountAsync() > 0)
                    return new FailureResponse("Email or Mobile Number already registered!");

                //If Email or Mobile Number is already not exist the add User details
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new SuccessResponse("User Registration Successful");
            }
            catch (Exception ex)
            {
                // Will Do some logging stuff here
                return new FailureResponse($"An error occurred when saving the user record: {ex.Message}");
            }
        }
    }
}
