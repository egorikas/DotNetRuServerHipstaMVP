using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(5)]
    public class AddApplicationReadyFlag : Migration
    {
        public override void Up()
        {
            Create
                .Column("IsUserVisible")
                .OnTable("Speakers")
                .AsBoolean()
                .WithDefaultValue(false);
            Create
                .Column("IsUserVisible")
                .OnTable("Talks")
                .AsBoolean()
                .WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete.Column("IsUserVisible").FromTable("Speakers");
            Delete.Column("IsUserVisible").FromTable("Talks");
        }
    }
}