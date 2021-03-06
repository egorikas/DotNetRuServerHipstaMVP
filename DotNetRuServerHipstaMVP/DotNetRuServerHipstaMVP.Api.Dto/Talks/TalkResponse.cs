using System.Collections.Generic;
using DotNetRuServerHipstaMVP.Api.Dto.Speakers;

namespace DotNetRuServerHipstaMVP.Api.Dto.Talks
{
    public class TalkResponse
    {
        public int Id { get; set; }
        public string ExportId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string CodeUrl { get; set; }
        public string SlidesUrl { get; set; }
        public string VideoUrl { get; set; }

        public bool IsDraft { get; set; }

        public List<SpeakerResponse> Speakers { get; set; }
    }
}