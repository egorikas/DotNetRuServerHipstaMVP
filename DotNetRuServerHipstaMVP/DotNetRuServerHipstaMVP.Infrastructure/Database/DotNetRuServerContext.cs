using Microsoft.EntityFrameworkCore;

namespace DotNetRuServerHipstaMVP.Infrastructure
{
    public class DotNetRuServerContext : DbContext
    {
        public DotNetRuServerContext(DbContextOptions options)
            : base(options)
        {
        }

    }
}