using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Domain.Entities;

namespace DotNetRuServerHipstaMVP.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity:IEntity
    {
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
 
        Task<TEntity> GetById(string id, params Expression<Func<TEntity, object>>[] includes);
 
        Task Create(TEntity entity);
 
        Task Delete(string id);
    }
}