using System.Threading.Tasks;

namespace DotNetRuServerHipstaMVP.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}