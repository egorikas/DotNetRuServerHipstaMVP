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

        Task<List<Speaker>> GetListAsync(bool takeForMobile, int skip, int take,
            params Expression<Func<Speaker, object>>[] includes);

        Task<Speaker> GetByIdAsync(string id);
        Task<Speaker> GetByIdWithTalksAsync(string id);

        Task<string> AddAsync(Speaker speaker);
    }
}