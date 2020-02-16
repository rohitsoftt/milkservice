using MilkService.API.Domain.Repositories;
using MilkService.API.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Persistence.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly MilkServiceContext _context;

        public UnitOfWork(MilkServiceContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
