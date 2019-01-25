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
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly DotNetRuServerContext _context;

        public SpeakerRepository(DotNetRuServerContext context)
        {
            _context = context;
        }

        public Task<int> CountAsync()
        {
            return _context.Speakers.CountAsync();
        }

        public Task<int> CountAsync(Expression<Func<Speaker, bool>> predicate)
        {
            return _context.Speakers.CountAsync(predicate);
        }

        public Task<List<Speaker>> GetListAsync(
            bool onlyUserVisible,
            int skip,
            int take,
            params Expression<Func<Speaker, object>>[] includes
        )
        {
            var query = _context.Speakers.Includes(includes);

            if (onlyUserVisible) query = query.Where(x => x.IsUserVisible);

            return query.Skip(skip)
                .Take(take).ToListAsync();
        }

        public Task<Speaker> GetByIdAsync(int id)
        {
            return _context.Speakers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Speaker> GetByIdWithTalksAsync(int id)
        {
            return _context.Speakers.Include(x => x.Talks).ThenInclude(x => x.Talk)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<int> AddAsync(Speaker speaker)
        {
            await _context.Speakers.AddAsync(speaker);
            return speaker.Id;
        }
    }
}