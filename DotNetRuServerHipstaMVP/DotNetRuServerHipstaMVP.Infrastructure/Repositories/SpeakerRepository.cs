using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Domain.Entities;
using DotNetRuServerHipstaMVP.Domain.Interfaces;
using DotNetRuServerHipstaMVP.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DotNetRuServerHipstaMVP.Infrastructure
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly DotNetRuServerContext _context;

        public SpeakerRepository(DotNetRuServerContext context)
        {
            _context = context;
        }

        public Task<int> CountAsync() => _context.Speakers.CountAsync();

        public Task<List<Speaker>> GetListAsync(int skip, int take, params Expression<Func<Speaker, object>>[] includes)
        {
            return _context.Speakers.Includes(includes).Skip(skip).Take(take).ToListAsync();
        }

        public Task<Speaker> GetByIdAsync(string id)
        {
            return _context.Speakers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Speaker> GetByIdWithRelationsAsync(string id)
        {
            return _context.Speakers.Include(x => x.Talks).ThenInclude(x => x.Talk)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<string> AddAsync(Speaker speaker)
        {
            speaker.Id = "temp";
            await _context.Speakers.AddAsync(speaker);
            return speaker.Id;
        }
    }
}