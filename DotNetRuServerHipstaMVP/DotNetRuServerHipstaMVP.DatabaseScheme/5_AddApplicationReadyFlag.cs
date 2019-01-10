using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    public class AddApplicationReadyFlag : Migration
    {
        public override void Up()
        {
            Create.Column("IsUserVisible").OnTable("Speakers");
        }

        public override void Down()
        {
            Delete.Column("IsUserVisible").FromTable("Speakers");
        }
    }
}