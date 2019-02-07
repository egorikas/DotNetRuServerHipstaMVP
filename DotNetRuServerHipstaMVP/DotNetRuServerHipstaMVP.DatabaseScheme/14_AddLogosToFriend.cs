using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(14)]
    public class AddLogosToFriend : Migration
    {
        public override void Up()
        {
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

        public override void Down()
        {
            Delete.Column("Logo").FromTable("Friends");
            Delete.Column("SmallLogo").FromTable("Friends");
        }
    }
}