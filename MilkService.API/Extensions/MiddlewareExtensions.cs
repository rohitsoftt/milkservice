using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using MilkService.API.Domain.Exceptions;
using MilkService.API.Domain.Models.DBModels.UserModels;
using MilkService.API.Domain.Models.Queries.Response.User;
using MilkService.API.Domain.Repositories;
using MilkService.API.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                //o.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("X-version"), new QueryStringApiVersionReader("api-version"));
                o.ErrorResponses = new ErrorResponseProvider();
            });
            return services;
        }
    }
    public class UserDetailsMiddleware
    {
        private readonly RequestDelegate _next;

        public UserDetailsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IUserDetails userDetails, MilkServiceContext context)
        {
            //Alternative method
            //var userDetails = httpContext.RequestServices.GetService<IUserDetails>();
            if (httpContext.Request.Headers.TryGetValue("Token", out StringValues stoken))
            {
                var token = stoken.SingleOrDefault();
                var user = (from u in context.User
                            join session in context.UserSession on u.Id equals session.UserId
                            where session.Token.Equals(token)
                            select u).FirstOrDefault();

                if (user != null)
                {
                    //userDetails = iMapper.Map<User, UserDetails>(user);
                    userDetails.Id = user.Id;
                    userDetails.FirstName = user.FirstName;
                    userDetails.LastName = user.LastName;
                    userDetails.Email = user.Email;
                    userDetails.MobileNo = user.MobileNo;
                    userDetails.Address = user.Address;
                    userDetails.UserRole = user.UserRole;
                    userDetails.PINCode = user.Pincode;
                }
            }
            await _next.Invoke(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SecretKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserDetailsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserDetailsMiddleware>();
        }
    }
}