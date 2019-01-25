using System.Collections.Generic;

namespace DotNetRuServerHipstaMVP.Domain.Entities
{
    public class Talk : IDraftable, IUserVisible
    {
        public int Id { get; set; }
        public string ExportId { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }

        public string CodeUrl { get; set; }
        public string SlidesUrl { get; set; }
        public string VideoUrl { get; set; }

        public ICollection<SpeakerTalk> Speakers { get; set; }
        public ICollection<SeeAlsoTalk> SeeAlsoTalks { get; set; }

        public bool IsDraft { get; set; }
        public bool IsUserVisible { get; set; }
    }
}