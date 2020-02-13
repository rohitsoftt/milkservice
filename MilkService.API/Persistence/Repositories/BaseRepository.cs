using MilkService.API.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly MilkServiceContext _context;

        public BaseRepository(MilkServiceContext context)
        {
            _context = context;
        }
    }
}
