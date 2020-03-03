using MilkService.API.Domain.Models.Queries;
using MilkService.API.Domain.Models.Queries.UserQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Resources.UserResource
{
    public class CustomerUserQueryResource: QueryResource
    {
        public int? UserId { get; set; }
    }
}
