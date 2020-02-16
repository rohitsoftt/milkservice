using AutoMapper;
using MilkService.API.Domain.Models;
using MilkService.API.Resources.UserResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Mapping
{
    public class ResourceToModelProfile: Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<RegisterUserResource, User>();
        }
    }
}
