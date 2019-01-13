using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(1)]
    public class AddSpeakerTable : Migration
    {
        public override void Up()
        {
            var table = Create.Table("Speakers");
            table
                .WithColumn("Id")
                .AsString(100)
                .NotNullable()
                .PrimaryKey();
            table
                .WithColumn("Name")
                .AsString(100)
                .NotNullable();
            table
                .WithColumn("CompanyName")
                .AsString(100);
            table
                .WithColumn("CompanyUrl")
                .AsString(100);
            table
                .WithColumn("Description")
                .AsString(100);
            table
                .WithColumn("BlogUrl")
                .AsString(100);
            table
                .WithColumn("ContactsUrl")
                .AsString(100);
            table
                .WithColumn("TwitterUrl")
                .AsString(50);
            table
                .WithColumn("HabrUrl")
                .AsString(50);
            table
                .WithColumn("GithubUrl")
                .AsString(50);
        }

        public override void Down()
        {
            Delete.Table("Speakers");
        }
    }
}