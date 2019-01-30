using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    public class AddApplicationReadyFlag : Migration
    {
        public override void Up()
        {
            Create
                .Column("IsUserVisible")
                .OnTable("Speakers")
                .AsBoolean()
                .WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete.Column("IsUserVisible").FromTable("Speakers");
        }
    }
}