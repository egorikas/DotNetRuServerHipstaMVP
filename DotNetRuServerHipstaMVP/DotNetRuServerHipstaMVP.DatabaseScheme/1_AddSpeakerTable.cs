using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(1)]
    public class AddSpeakerTable  : Migration
    {
        public override void Up()
        {
            var table = Create.Table("speakers");
            table
                .WithColumn("id")
                .AsString(100)
                .NotNullable()
                .PrimaryKey();         
            table
                .WithColumn("name")
                .AsString(100)
                .NotNullable();           
            table
                .WithColumn("company_name")
                .AsString(100);            
            table
                .WithColumn("company_url")
                .AsString(100);            
            table
                .WithColumn("description")
                .AsString(100);
            table
                .WithColumn("blog_url")
                .AsString(100);
            table
                .WithColumn("contacts_url")
                .AsString(100);
            table
                .WithColumn("twitter_url")
                .AsString(50);
            table
                .WithColumn("habr_url")
                .AsString(50);
            table
                .WithColumn("github_url")
                .AsString(50);
        }

        public override void Down()
        {
            Delete.Table("speakers");
        }
    }
}