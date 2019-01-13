using System.ComponentModel.DataAnnotations;

namespace DotNetRuServerHipstaMVP.Api.Dto.Talks
{
    public class UpsertTalkRequest
    {
        [Required(ErrorMessage = "Заголовок должен быть заполнен")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string CodeUrl { get; set; }
        public string SlidesUrl { get; set; }
        public string VideoUrl { get; set; }

        public bool IsDraft { get; set; }
        public bool IsUserVisible { get; set; }
    }
}