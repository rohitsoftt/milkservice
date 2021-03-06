﻿using Microsoft.EntityFrameworkCore;
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
using MilkService.API.Helpers;

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
                //Checking Email or Mobile number is already exist or not in database
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
                return new FailureResponse($"Internal Service Error: {ex.Message}");
            }
        }
        public async Task<ServiceResponse<UserLoginDetails>> LoginAsync(LoginUserResource loginUserResource)
        {
            try
            {
                var user = await _userRepository.LoginAsync(loginUserResource);
                if (user != null)
                {
                    var userDetails = _mapper.Map<User, UserLoginDetails>(user);
                    var token = Guid.NewGuid().ToString();
                    await _userRepository.CreateSession(user.Id, token);
                    await _unitOfWork.CompleteAsync();
                    userDetails.Token = token;
                    return new ServiceResponse<UserLoginDetails>(userDetails);
                }
                else
                {
                    return new ServiceResponse<UserLoginDetails>("Invalid Email or Password");
                }
            }
            catch(Exception ex)
            {
                return new ServiceResponse<UserLoginDetails>($"Internal Service Error: {ex.Message}");
            }
        }
        public async Task<ServiceResponse<User>> UpdateProfile(User user)
        {
            try
            {
                if (await _context.User.Where(i => (i.Email == user.Email || i.MobileNo == user.MobileNo) && i.Id!=user.Id).CountAsync() > 0)
                    return new ServiceResponse<User>("Email or Mobile Number already registered!");
                //await _unitOfWork.CompleteAsync();
                await _userRepository.UpdateProfile(user);
                await _unitOfWork.CompleteAsync();
                return new ServiceResponse<User>(user);
            }
            catch(Exception ex)
            {
                return new ServiceResponse<User>($"Internal Service Error: {ex.Message}");
            }
        }
        public async Task<OResponse> UpdatePasswordAsync(int id, string password, string oldPassword)
        {
            try
            {
                oldPassword = oldPassword.GetHashed();
                if(_context.User.Where(i=>i.Id==id && i.Password == oldPassword).Count() > 0)
                {
                    password = password.GetHashed();
                    await _userRepository.UpdatePassword(id, password);
                    await _unitOfWork.CompleteAsync();
                    return new SuccessResponse("Password successfully updated");
                }
                else
                {
                    return new FailureResponse("Old password is invalid");
                }
            }
            catch(Exception ex)
            {
                return new FailureResponse($"Failed, could not update password:{ex.Message}");
            }
        }
    }
}
