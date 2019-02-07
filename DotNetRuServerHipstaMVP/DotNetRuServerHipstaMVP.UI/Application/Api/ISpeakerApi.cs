using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Api.Dto.Speakers;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace DotNetRuServerHipstaMVP.UI.Application.Api
{
    public interface ISpeakerApi
    {
        [Get("/speakers")]
        Task<SpeakerListResponse> GetListAsync([Query] int? skip, [Query] int? take);
        [Get("/speakers/{speakerId}")]
        Task<SpeakerResponse> GetSpeakerAsync(string speakerId);
        
        [Post("/speakers")]
        Task<string> AddSpeakerAsync([Body]UpsertSpeakerRequest request);

        [Put("/speakers/{speakerId}")]
        Task<StatusCodeResult> UpdateSpeakerAsync(string speakerId, [Body] UpsertSpeakerRequest request);
    }
}