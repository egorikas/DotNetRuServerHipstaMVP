namespace DotNetRuServerHipstaMVP.Domain.Entities
{
    public class Friend
    {
        public int Id { get; set; }
        public string ExportId { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}