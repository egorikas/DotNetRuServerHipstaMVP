using System.Net;
using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Api.Application.ExceptionFilter;
using DotNetRuServerHipstaMVP.Api.Application.Extensions;
using DotNetRuServerHipstaMVP.Api.Dto.Speakers;
using DotNetRuServerHipstaMVP.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRuServerHipstaMVP.Api.Controllers.Speakers
{
    [Produces("application/json")]
    [Route("/speakers")]
    public class SpeakersController : Controller
    {
        private readonly ISpeakerRepository _speakerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SpeakersController(ISpeakerRepository speakerRepository, IUnitOfWork unitOfWork)
        {
            _speakerRepository = speakerRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("/speakers")]
        [ProducesResponseType(typeof(SpeakerListResponse), (int) HttpStatusCode.OK)]
        public async Task<SpeakerListResponse> GetListAsync([FromQuery] int? skip, [FromQuery] int? take)
        {
            var count = await _speakerRepository.CountAsync(x => x.IsUserVisible);
            var speakers = await _speakerRepository.GetListAsync(true, skip ?? 0, take ?? count);
            return speakers.CreateSpeakerListResponse(count);
        }

        [Authorize]
        [HttpGet]
        [Route("/speakers/draft")]
        [ProducesResponseType(typeof(SpeakerListResponse), (int) HttpStatusCode.OK)]
        public async Task<SpeakerListResponse> GetListForNonAppAsync([FromQuery] int? skip, [FromQuery] int? take)
        {
            var count = await _speakerRepository.CountAsync();
            var speakers = await _speakerRepository.GetListAsync(false, skip ?? 0, take ?? count);
            return speakers.CreateSpeakerListResponse(count);
        }

        [HttpGet]
        [Route("/speakers/{speakerId}")]
        [ProducesResponseType(typeof(SpeakerResponse), (int) HttpStatusCode.OK)]
        public async Task<SpeakerResponse> GetSpeakerAsync(int speakerId)
        {
            var speaker = await _speakerRepository.GetByIdWithTalksAsync(speakerId);
            return speaker.CreateSpeakerResponse();
        }

        [Authorize]
        [HttpPost]
        [Route("/speakers")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotAcceptable)]
        public Task<int> AddSpeakerAsync([FromBody] UpsertSpeakerRequest request)
        {
            this.ValidateRequest(request);
            // TODO MAKE GENERATION OF ID
            var newSpeaker = request.CreateSpeaker();
            return _speakerRepository.AddAsync(newSpeaker);
        }

        [Authorize]
        [HttpPut]
        [Route("/speakers/{speakerId}")]
        [ProducesResponseType(typeof(OkResult), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotAcceptable)]
        public async Task<StatusCodeResult> UpdateSpeakerAsync(int speakerId,
            [FromBody] UpsertSpeakerRequest request)
        {
            this.ValidateRequest(request);

            var savedSpeaker = await _speakerRepository.GetByIdAsync(speakerId);
            savedSpeaker.Name = request.Name;
            savedSpeaker.Description = request.Description;
            savedSpeaker.BlogUrl = request.BlogUrl;
            savedSpeaker.HabrUrl = request.HabrUrl;
            savedSpeaker.CompanyUrl = request.CompanyUrl;
            savedSpeaker.TwitterUrl = request.TwitterUrl;
            savedSpeaker.CompanyName = request.CompanyName;
            savedSpeaker.ContactsUrl = request.ContactsUrl;
            savedSpeaker.GitHubUrl = request.GitHubUrl;

            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
    }
}