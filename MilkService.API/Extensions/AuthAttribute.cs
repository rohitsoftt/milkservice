using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using MilkService.API.Domain.Models.DBModels.UserModels;
using MilkService.API.Domain.Models.Queries.Response.User;
using MilkService.API.Domain.Services.Communication.Response;
using MilkService.API.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MilkService.API.Domain.Repositories;

namespace MilkService.API.Extensions
{
    public class AuthAttribute: TypeFilterAttribute
    {
        public AuthAttribute(UserRoles role = UserRoles.Customer) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { role };
        }
    }
    public class ClaimRequirementFilter : ActionFilterAttribute
    {
        private readonly MilkServiceContext _context;
        private readonly IMapper _iMapper;
        private readonly IUserDetails _userDetails;
        private readonly UserRoles _role;
        private readonly IUserRepository _userRepository;
        public ClaimRequirementFilter(UserRoles role, IUserDetails userDetails, IUserRepository userRepository)
        {
            _role = role;
            _userDetails = userDetails;
            _userRepository = userRepository;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext actionContext, ActionExecutionDelegate next)
        {
            if (_userDetails.UserRole != null)
            {
                UserRoles role = (UserRoles)Int32.Parse(_userDetails.UserRole);
                if (string.IsNullOrEmpty(_userDetails.UserRole))
                    actionContext.Result = new BadRequestObjectResult(new FailureResponse("Unauthorized access"));
                else if (!(_role == role))
                    actionContext.Result = new UnauthorizedObjectResult(new FailureResponse("You don't have access to this page"));
                else
                {
                    await _userRepository.ExtendToken(_userDetails.Id);
                    await next();
                }
            }
            else
            {
                actionContext.Result = new BadRequestObjectResult(new FailureResponse("Unauthorized access"));
            }
        }
    }
}
