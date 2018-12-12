namespace DotNetRuServerHipstaMVP.Domain.Entities
{
    public class SeeAlsoTalk
    {
        public string ParentTalkId { get; set; }
        public Talk ParentTalk { get; set; }
        public string ChildTalkId { get; set; }
        public Talk ChildTalk { get; set; }
    }
}