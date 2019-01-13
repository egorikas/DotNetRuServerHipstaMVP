using System.ComponentModel.DataAnnotations;

namespace DotNetRuServerHipstaMVP.Api.Dto.Talks
{
    public class LinkSpeakerToTalkRequest
    {
        [Required(ErrorMessage = "SpeakerId должен быть заполнен")]
        public string SpeakerId { get; set; }
    }
}