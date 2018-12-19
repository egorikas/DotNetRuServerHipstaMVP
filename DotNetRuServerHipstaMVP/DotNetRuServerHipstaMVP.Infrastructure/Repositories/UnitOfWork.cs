using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Domain.Interfaces;
using DotNetRuServerHipstaMVP.Infrastructure.Database;

namespace DotNetRuServerHipstaMVP.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DotNetRuServerContext _context;

        public UnitOfWork(DotNetRuServerContext context)
        {
            _context = context;
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}