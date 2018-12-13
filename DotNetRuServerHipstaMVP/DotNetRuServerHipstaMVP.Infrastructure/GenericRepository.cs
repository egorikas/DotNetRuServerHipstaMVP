using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Domain.Entities;
using DotNetRuServerHipstaMVP.Domain.Interfaces;
using DotNetRuServerHipstaMVP.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DotNetRuServerHipstaMVP.Infrastructure
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DotNetRuServerContext _context;

        public GenericRepository(DotNetRuServerContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            return _context.Set<TEntity>().Includes(includes);
        }

        public Task<TEntity> GetById(string id, params Expression<Func<TEntity, object>>[] includes)
        {
            return _context.Set<TEntity>()
                .Includes(includes)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task Create(TEntity entity)
        {
            return _context.Set<TEntity>().AddAsync(entity);
        }


        public async Task Delete(string id)
        {
            var entity = await GetById(id);
            _context.Set<TEntity>().Remove(entity);
        }
    }
}