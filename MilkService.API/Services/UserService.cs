using Microsoft.EntityFrameworkCore;
using MilkService.API.Domain.Models;
using MilkService.API.Domain.Repositories;
using MilkService.API.Domain.Services;
using MilkService.API.Domain.Services.Communication;
using MilkService.API.Domain.Services.Communication.Response;
using MilkService.API.Persistence.Contexts;
using MilkService.API.Resources.UserResource;
using MilkService.API.Domain.Models.DBModels.UserModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MilkService.API.Domain.Models.Queries.Response.User;

namespace MilkService.API.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MilkServiceContext _context;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, MilkServiceContext context, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
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
        public async Task<UserLoginResponse> LoginAsync(LoginUserResource loginUserResource)
        {
            var user = await _userRepository.LoginAsync(loginUserResource);
            if (user != null)
            {
                var userDetails = _mapper.Map<User, UserLoginDetails>(user);
                var token =  Guid.NewGuid().ToString();
                await _userRepository.CreateSession(user.Id, token);
                await _unitOfWork.CompleteAsync();
                userDetails.Token = token;
                return new UserLoginResponse(userDetails);
            }
            else
            {
                return new UserLoginResponse("Invalid Email or Password");
            }

        }
    }
}
