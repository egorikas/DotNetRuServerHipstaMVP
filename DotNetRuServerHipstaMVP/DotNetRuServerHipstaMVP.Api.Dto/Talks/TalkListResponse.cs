using System.Collections.Generic;

namespace DotNetRuServerHipstaMVP.Api.Dto.Talks
{
    public class TalkListResponse
    {
        public int Count { get; set; }
        public List<TalkResponse> Talks { get; set; }
    }
}