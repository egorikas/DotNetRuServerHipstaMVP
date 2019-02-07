using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Domain.Entities;

namespace DotNetRuServerHipstaMVP.Domain.Interfaces
{
    public interface ISpeakerRepository
    {
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<Speaker, bool>> predicate);

        Task<List<Speaker>> GetListAsync(bool onlyUserVisible,
            int skip,
            int take,
            params Expression<Func<Speaker, object>>[] includes);

        Task<Speaker> GetByIdAsync(int id);
        Task<Speaker> GetByIdWithTalksAsync(int id);

        Task<int> AddAsync(Speaker speaker);
    }
}