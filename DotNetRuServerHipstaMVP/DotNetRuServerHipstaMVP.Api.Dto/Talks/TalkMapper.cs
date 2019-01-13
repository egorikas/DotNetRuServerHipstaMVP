using System.Collections.Generic;
using DotNetRuServerHipstaMVP.Domain.Entities;

namespace DotNetRuServerHipstaMVP.Api.Dto.Talks
{
    public static class TalkMapper
    {
        public static TalkResponse ToTalkResponse(this Talk talk)
        {
            return new TalkResponse
            {
                Id = talk.Id,
                Description = talk.Description,
                Title = talk.Title,
                CodeUrl = talk.CodeUrl,
                IsDraft = talk.IsDraft,
                VideoUrl = talk.VideoUrl,
                SlidesUrl = talk.SlidesUrl
            };
        }

        public static TalkListResponse ToTalkListResponse(this ICollection<Talk> talks, int count)
        {
            var resultList = new List<TalkResponse>();
            foreach (var talk in talks) resultList.Add(talk.ToTalkResponse());

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