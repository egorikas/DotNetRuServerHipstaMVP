using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Api.Application.ExceptionFilter;
using DotNetRuServerHipstaMVP.Api.Application.Extensions;
using DotNetRuServerHipstaMVP.Api.Dto.Talks;
using DotNetRuServerHipstaMVP.Domain.Entities;
using DotNetRuServerHipstaMVP.Domain.Exceptions;
using DotNetRuServerHipstaMVP.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRuServerHipstaMVP.Api.Controllers.Talks
{
    [Produces("application/json")]
    [Route("/talks")]
    public class TalksController : Controller
    {
        private readonly ISpeakerRepository _speakerRepository;
        private readonly ITalkRepository _talkRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TalksController(ITalkRepository talkRepository, IUnitOfWork unitOfWork,
            ISpeakerRepository speakerRepository)
        {
            _talkRepository = talkRepository;
            _unitOfWork = unitOfWork;
            _speakerRepository = speakerRepository;
        }

        [HttpGet]
        [Route("/talks")]
        [ProducesResponseType(typeof(TalkListResponse), (int) HttpStatusCode.OK)]
        public async Task<TalkListResponse> GetListAsync([FromQuery] int? skip, [FromQuery] int? take)
        {
            var count = await _talkRepository.CountAsync(x => x.IsUserVisible && !x.IsDraft);
            var talks = await _talkRepository.GetListAsync(true, false, skip ?? 0, take ?? count);
            return talks.ToTalkListResponse(count);
        }

        [Authorize]
        [HttpGet]
        [Route("/talks/draft")]
        [ProducesResponseType(typeof(TalkListResponse), (int) HttpStatusCode.OK)]
        public async Task<TalkListResponse> GetListForNonAppAsync([FromQuery] int? skip, [FromQuery] int? take)
        {
            var count = await _talkRepository.CountAsync(null);
            var talks = await _talkRepository.GetListAsync(false, true, skip ?? 0, take ?? count);
            return talks.ToTalkListResponse(count);
        }

        [HttpGet]
        [Route("/talks/{talkId}")]
        [ProducesResponseType(typeof(TalkResponse), (int) HttpStatusCode.OK)]
        public async Task<TalkResponse> GetTalkAsync(string talkId)
        {
            if (string.IsNullOrEmpty(talkId))
                throw new ValidationException("talkId должно быть задано");

            var talk = await _talkRepository.GetByIdWithSpeakersAsync(talkId);
            return talk.ToTalkResponse();
        }

        [Authorize]
        [HttpPost]
        [Route("/talks")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotAcceptable)]
        public Task<string> AddSpeakerAsync([FromBody] UpsertTalkRequest request)
        {
            this.ValidateRequest(request);

            var newTalk = request.CreateTalk();
            return _talkRepository.AddAsync(newTalk);
        }

        [Authorize]
        [HttpPut]
        [Route("/talks/{talkId}")]
        [ProducesResponseType(typeof(OkResult), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotAcceptable)]
        public async Task<StatusCodeResult> UpdateSpeakerAsync(string talkId,
            [FromBody] UpsertTalkRequest request)
        {
            this.ValidateRequest(request);
            if (string.IsNullOrEmpty(talkId))
                throw new ValidationException("talkId должно быть задано");

            var savedTalk = await _talkRepository.GetByIdAsync(talkId);
            if (savedTalk == null)
                throw new NotFoundException("Доклада не существует");

            savedTalk.Title = request.Title;
            savedTalk.Description = request.Description;
            savedTalk.CodeUrl = request.CodeUrl;
            savedTalk.IsDraft = request.IsDraft;
            savedTalk.VideoUrl = request.VideoUrl;
            savedTalk.SlidesUrl = request.SlidesUrl;
            savedTalk.IsUserVisible = request.IsUserVisible;

            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("/talks/{talkId}/speakers")]
        [ProducesResponseType(typeof(OkResult), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotAcceptable)]
        public async Task<StatusCodeResult> LinkSpeakerToTalk(string talkId, LinkSpeakerToTalkRequest request)
        {
            this.ValidateRequest(request);
            if (string.IsNullOrEmpty(talkId))
                throw new ValidationException("talkId должно быть задано");

            var savedTalk = await _talkRepository.GetByIdAsync(talkId);
            if (savedTalk == null)
                throw new NotFoundException("Доклада не существует");

            var savedSpeaker = await _speakerRepository.GetByIdWithTalksAsync(request.SpeakerId);
            if (savedSpeaker == null)
                throw new NotFoundException("Спикера не существует");

            if (savedSpeaker.Talks == null)
                savedSpeaker.Talks = new List<SpeakerTalk>();

            savedSpeaker.Talks.Add(new SpeakerTalk
            {
                Talk = savedTalk,
                Speaker = savedSpeaker
            });

            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("/talks/{talkId}/speakers/{speakerId}")]
        [ProducesResponseType(typeof(OkResult), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotAcceptable)]
        public async Task<StatusCodeResult> LinkSpeakerToTalk(string talkId, string speakerId)
        {
            if (string.IsNullOrEmpty(talkId))
                throw new ValidationException("talkId должно быть задано");
            if (string.IsNullOrEmpty(speakerId))
                throw new ValidationException("speakerId должно быть задано");

            await _talkRepository.UnlinkSpeaker(talkId, speakerId);

            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
    }
}