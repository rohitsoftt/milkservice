using AutoMapper;
using MilkService.API.Domain.Models.DBModels.UserModels;
using MilkService.API.Domain.Models.Queries.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserLoginDetails>();
            CreateMap<User, UserDetails>();
        }
    }
}
