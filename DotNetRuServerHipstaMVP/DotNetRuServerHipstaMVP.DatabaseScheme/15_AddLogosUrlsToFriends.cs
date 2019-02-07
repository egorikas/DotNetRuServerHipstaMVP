using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(15)]
    public class AddLogosUrlsToFriend : Migration
    {
        public override void Up()
        {
            Delete.Column("Logo").FromTable("Friends");
            Delete.Column("SmallLogo").FromTable("Friends");

            Create
                .Column("LogoUrl")
                .OnTable("Friends")
                .AsString(500)
                .NotNullable();

            Create
                .Column("SmallLogoUrl")
                .OnTable("Friends")
                .AsString(500)
                .NotNullable();
        }

        public override void Down()
        {
            Delete.Column("LogoUrl").FromTable("Friends");
            Delete.Column("SmallLogoUrl").FromTable("Friends");

            Create
                .Column("Logo")
                .OnTable("Friends")
                .AsBinary()
                .NotNullable();

            Create
                .Column("SmallLogo")
                .OnTable("Friends")
                .AsBinary()
                .NotNullable();
        }
    }
}