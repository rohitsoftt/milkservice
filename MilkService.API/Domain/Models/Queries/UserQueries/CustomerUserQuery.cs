using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Domain.Models.Queries.UserQueries
{
    public class CustomerUserQuery: Query
    {
        public int? UserId { get; set; }

        public CustomerUserQuery(int? userId, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            UserId = userId;
        }
    }
}
