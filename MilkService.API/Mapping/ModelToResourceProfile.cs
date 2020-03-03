using AutoMapper;
using MilkService.API.Domain.Models.DBModels.UserModels;
using MilkService.API.Domain.Models.Queries;
using MilkService.API.Domain.Models.Queries.Response.User;
using MilkService.API.Resources.UserResource;
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
            CreateMap<QueryResult<User>, QueryResultResource<UserDetails>>();
        }
    }
}
