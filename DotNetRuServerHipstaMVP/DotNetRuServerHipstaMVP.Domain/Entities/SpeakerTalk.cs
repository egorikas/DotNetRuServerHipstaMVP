namespace DotNetRuServerHipstaMVP.Domain.Entities
{
    public class SpeakerTalk
    {
        public string SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
        
        public string TalkId { get; set; }
        public Talk Talk { get; set; }
    }
}