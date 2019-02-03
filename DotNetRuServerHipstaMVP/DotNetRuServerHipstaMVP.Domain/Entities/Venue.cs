using System.Collections.Generic;

namespace DotNetRuServerHipstaMVP.Domain.Entities
{
    public class Venue
    {
        public int Id { get; set; }
        public string ExportId { get; set; }

        public string Name { get; set; }
        public string MapUrl { get; set; }
        public string Address { get; set; }

        public List<Meetup> Meetups { get; set; }
    }
}