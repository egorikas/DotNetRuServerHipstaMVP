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

        Task<Talk> GetByIdAsync(int id);
        Task<Talk> GetByIdWithSpeakersAsync(int id);

        Task<int> AddAsync(Talk talk);

        Task UnlinkSpeaker(int talkId, int speakerId);
    }
}