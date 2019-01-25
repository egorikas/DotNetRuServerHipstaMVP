using System;

namespace DotNetRuServerHipstaMVP.Domain.Entities
{
    public class Meetup
    {
        public int Id { get; set; }
        public string ExportId { get; set; }
        
        public string Name { get; set; }
        public Session[] Sessions { get; set; }
        
    }
    
    public class Session {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

//class Session
//{
//    [string] $TalkId
//        [DateTime] $StartTime
//        [DateTime] $EndTime
//}


//[string] $Name
//[string] $CommunityId
//
//# Obsolete. Use Sessions property
//[DateTime] $Date
//
//[string[]] $FriendIds
//[string] $VenueId
//[Session[]] $Sessions
//
//# Obsolete. Use Sessions property
//[string[]] $TalkIds
