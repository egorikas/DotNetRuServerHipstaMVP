﻿using System.Collections.Generic;

namespace DotNetRuServerHipstaMVP.Domain.Entities
{
    public class Talk : IDraftable, IEntity, IUserVisible
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string CodeUrl { get; set; }
        public string SlidesUrl { get; set; }
        public string VideoUrl { get; set; }

        public ICollection<SpeakerTalk> Speakers { get; set; }
        public ICollection<SeeAlsoTalk> SeeAlsoTalks { get; set; }

        public bool IsDraft { get; set; }
        public string Id { get; set; }
        public bool IsUserVisible { get; set; }
    }
}