using System.Collections.Generic;
using System.Linq;
using DotNetRuServerHipstaMVP.Api.Dto.Speakers;
using DotNetRuServerHipstaMVP.Domain.Entities;

namespace DotNetRuServerHipstaMVP.Api.Dto.Talks
{
    public static class TalkMapper
    {
        public static TalkResponse ToTalkResponse(this Talk talk)
        {
            var response = new TalkResponse
            {
                Id = talk.Id,
                ExportId = talk.ExportId,
                Description = talk.Description,
                Title = talk.Title,
                CodeUrl = talk.CodeUrl,
                IsDraft = talk.IsDraft,
                VideoUrl = talk.VideoUrl,
                SlidesUrl = talk.SlidesUrl
            };

            if (talk.Speakers == null) return response;


            response.Speakers = new List<SpeakerResponse>();
            foreach (var talkSpeaker in talk.Speakers)
                response.Speakers.Add(talkSpeaker.Speaker.CreateSpeakerResponse());

            return response;
        }

        public static TalkListResponse ToTalkListResponse(this ICollection<Talk> talks, int count)
        {
            var resultList = talks.Select(talk => talk.ToTalkResponse()).ToList();

            return new TalkListResponse
            {
                Talks = resultList,
                Count = count
            };
        }

        public static Talk CreateTalk(this UpsertTalkRequest request)
        {
            return new Talk
            {
                IsDraft = request.IsDraft,
                IsUserVisible = request.IsUserVisible,
                Title = request.Title,
                Description = request.Description,
                CodeUrl = request.Description,
                VideoUrl = request.VideoUrl,
                SlidesUrl = request.SlidesUrl
            };
        }
    }
}