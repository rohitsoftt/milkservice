using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilkService.API.Domain.Models;
using MilkService.API.Domain.Models.Queries.Response.User;
using MilkService.API.Domain.Services;
using MilkService.API.Domain.Services.Communication.Response;
using MilkService.API.Domain.Models.DBModels.UserModels;
using MilkService.API.Resources;
using MilkService.API.Resources.UserResource;
using MilkService.API.Extensions;

namespace MilkService.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUserDetails _userDetails;

        public UserController(IUserService userService, IMapper mapper, IUserDetails userDetails)
        {
            _userService = userService;
            _mapper = mapper;
            _userDetails = userDetails;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserResource registerUserResource)
        {
            try
            {
                var user = _mapper.Map<RegisterUserResource, User>(registerUserResource);
                var result = await _userService.SaveAsync(user);
                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailureResponse("Internal Server Error"));
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserResource loginUserResource)
        {
            try
            {
                var result = await _userService.LoginAsync(loginUserResource);
                if (!result.Success)
                {
                    return BadRequest(new FailureResponse(result.Message));
                }
                var userDeails = result.Resource;
                return Ok(new CResponse<UserLoginDetails>(result.Success, userDeails));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailureResponse("Internal Server Error"));
            }
        }
        [HttpGet]
        [Route("profile")]
        [Auth(UserRoles.Customer)]
        public IActionResult Profile()
        {
            return Ok(_userDetails);
        }
    }
}