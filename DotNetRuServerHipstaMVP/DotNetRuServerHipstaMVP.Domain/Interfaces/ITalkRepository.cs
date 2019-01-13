using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Domain.Entities;

namespace DotNetRuServerHipstaMVP.Domain.Interfaces
{
    public interface ITalkRepository
    {
        Task<int> CountAsync(Expression<Func<Talk, bool>> predicate);

        Task<List<Talk>> GetListAsync(bool onlyUserVisible,
            bool onlyNonDraft,
            int skip,
            int take,
            params Expression<Func<Talk, object>>[] includes);

        Task<Talk> GetByIdAsync(string id);
        Task<Talk> GetByIdWithSpeakersAsync(string id);

        Task<string> AddAsync(Talk talk);
    }
}