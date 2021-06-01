using OnlineBookLibrary.Lib.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Lib.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _ctx;

        public UnitOfWork(LibraryDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> Save()
        {
            return await _ctx.SaveChangesAsync() >= 0;
        }
    }
}
