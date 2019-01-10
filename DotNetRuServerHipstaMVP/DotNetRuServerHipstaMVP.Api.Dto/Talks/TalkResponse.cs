namespace DotNetRuServerHipstaMVP.Api.Dto.Talks
{
    public class TalkResponse
    {
        public string Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        
        public string CodeUrl { get; set; }
        public string SlidesUrl { get; set; }
        public string VideoUrl { get; set; } 
        
        public bool IsDraft { get; set; }
    }
}