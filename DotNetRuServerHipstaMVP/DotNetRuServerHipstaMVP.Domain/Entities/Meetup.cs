using System.Collections.Generic;
using System.Dynamic;

namespace DotNetRuServerHipstaMVP.Domain.Entities
{
    public class Meetup
    {
        public int Id { get; set; }
        public string ExportId { get; set; }

        public string Name { get; set; }
        public Session[] Sessions { get; set; }

        public Venue Venue { get; set; }
        public Community Community { get; set; }
        public List<Talk> Talks { get; set; }
        public List<FriendAtMeetup> Friends { get; set; }
    }
}