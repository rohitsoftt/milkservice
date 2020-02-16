using MilkService.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MilkService.API.Domain.Models.DBModels.UserModels;
using MilkService.API.Domain.Models.Queries.Response.User;

namespace MilkService.API.Domain.Services.Communication
{
    public class ServiceResponse<T> : BaseResponse<T>
    {
        public ServiceResponse(T obj): base(obj) { }
        public ServiceResponse(string message) : base(message) { }
    }
}
