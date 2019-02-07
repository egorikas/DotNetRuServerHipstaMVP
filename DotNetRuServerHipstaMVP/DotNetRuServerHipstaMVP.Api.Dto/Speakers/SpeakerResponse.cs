using System.Collections.Generic;
using DotNetRuServerHipstaMVP.Api.Dto.Talks;

namespace DotNetRuServerHipstaMVP.Api.Dto.Speakers
{
    public class SpeakerResponse
    {
        public int Id { get; set; }
        public string ExportId { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }
        public string Description { get; set; }

        public string BlogUrl { get; set; }
        public string ContactsUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string HabrUrl { get; set; }
        public string GitHubUrl { get; set; }

        public List<TalkResponse> Talks { get; set; }
    }
}