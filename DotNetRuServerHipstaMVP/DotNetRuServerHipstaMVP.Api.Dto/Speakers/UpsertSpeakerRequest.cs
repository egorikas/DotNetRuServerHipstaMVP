using System.ComponentModel.DataAnnotations;

namespace DotNetRuServerHipstaMVP.Api.Dto.Speakers
{
    public class UpsertSpeakerRequest
    {
        [Required(ErrorMessage = "Имя должно быть заполнено")]
        public string Name { get; set; }

        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }
        public string Description { get; set; }

        public string BlogUrl { get; set; }
        public string ContactsUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string HabrUrl { get; set; }
        public string GitHubUrl { get; set; }

        public bool IsUserVisible { get; set; }
    }
}