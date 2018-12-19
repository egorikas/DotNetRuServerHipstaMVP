using System.Collections.Generic;

namespace DotNetRuServerHipstaMVP.Api.Dto.Speakers
{
    public class SpeakerListResponse
    {
        public int Count { get; set; }
        public List<SpeakerResponse> Speakers { get; set; }
    }
}