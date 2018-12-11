﻿using System;
using System.Collections.Generic;

namespace DotNetRuServerHipstaMVP.Domain
{
    public class Talk : Draftable
    {
        public string Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        
        public string CodeUrl { get; set; }
        public string SlidesUrl { get; set; }
        public string VideoUrl { get; set; } 
        
        public ICollection<Speaker> Speakers { get; set; }
        public ICollection<Talk> SeeAlsoTalks { get; set; }
    }
}