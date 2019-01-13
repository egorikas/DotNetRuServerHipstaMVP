using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Domain.Entities;
using DotNetRuServerHipstaMVP.Domain.Interfaces;
using DotNetRuServerHipstaMVP.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DotNetRuServerHipstaMVP.Infrastructure.Repositories
{
    public class TalkRepository : ITalkRepository
    {
        private readonly DotNetRuServerContext _context;

        public TalkRepository(DotNetRuServerContext context)
        {
            _context = context;
        }

        public Task<int> CountAsync(Expression<Func<Talk, bool>> predicate)
        {
            return _context.Talks.CountAsync(predicate);
        }

        public Task<List<Talk>> GetListAsync(
            bool onlyUserVisible,
            bool onlyNonDraft,
            int skip,
            int take,
            params Expression<Func<Talk, object>>[] includes)
        {
            var query = _context.Talks.Includes(includes);

            if (onlyUserVisible) query = query.Where(x => x.IsUserVisible);
            if (onlyNonDraft) query = query.Where(x => !x.IsDraft);

            return query.Skip(skip).Take(take).ToListAsync();
        }

        public Task<Talk> GetByIdAsync(string id)
        {
            return _context.Talks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Talk> GetByIdWithSpeakersAsync(string id)
        {
            return _context.Talks.Include(x => x.Speakers).ThenInclude(x => x.Speaker)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<string> AddAsync(Talk talk)
        {
            talk.Id = "temp";
            await _context.Talks.AddAsync(talk);
            return talk.Id;
        }
    }
}