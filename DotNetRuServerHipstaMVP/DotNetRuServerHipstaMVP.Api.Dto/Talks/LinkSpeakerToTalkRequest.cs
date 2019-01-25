using System.ComponentModel.DataAnnotations;

namespace DotNetRuServerHipstaMVP.Api.Dto.Talks
{
    public class LinkSpeakerToTalkRequest
    {
        public int SpeakerId { get; set; }
    }
}