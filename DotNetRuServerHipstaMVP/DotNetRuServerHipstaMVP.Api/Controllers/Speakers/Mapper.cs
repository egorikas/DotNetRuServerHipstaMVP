using System.Collections.Generic;
using DotNetRuServerHipstaMVP.Api.Dto.Speakers;
using DotNetRuServerHipstaMVP.Domain.Entities;

namespace DotNetRuServerHipstaMVP.Api.Controllers.Speakers
{
    public static class Mapper
    {
        public static SpeakerListResponse CreateSpeakerListResponse(this List<Speaker> speakers, int allSpeakersCount)
        {
            var speakerResponses = new List<SpeakerResponse>();
            speakers.ForEach(x => speakerResponses.Add(x.CreateSpeakerResponse()));
            return new SpeakerListResponse
            {
                Count = allSpeakersCount,
                Speakers = speakerResponses
            };
        }

        public static SpeakerResponse CreateSpeakerResponse(this Speaker speaker)
        {
            return new SpeakerResponse
            {
                Id = speaker.Id,
                Name = speaker.Name,
                Description = speaker.Description,
                BlogUrl = speaker.BlogUrl,
                HabrUrl = speaker.HabrUrl,
                CompanyUrl = speaker.CompanyUrl,
                TwitterUrl = speaker.TwitterUrl,
                CompanyName = speaker.CompanyName,
                ContactsUrl = speaker.ContactsUrl,
                GitHubUrl = speaker.GitHubUrl
            };
        }

        public static Speaker CreateSpeaker(this UpsertSpeakerRequest request)
        {
            return new Speaker
            {
                Name = request.Name,
                Description = request.Description,
                BlogUrl = request.BlogUrl,
                HabrUrl = request.HabrUrl,
                CompanyUrl = request.ContactsUrl,
                TwitterUrl = request.TwitterUrl,
                CompanyName = request.CompanyName,
                ContactsUrl = request.ContactsUrl,
                GitHubUrl = request.GitHubUrl
            };
        } 
    }
}