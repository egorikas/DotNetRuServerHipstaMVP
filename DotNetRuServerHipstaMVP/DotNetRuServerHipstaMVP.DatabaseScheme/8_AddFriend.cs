using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(8)]
    public class AddFriend : Migration
    {
        public override void Up()
        {
            var table = Create.Table("Friends");
            table
                .WithColumn("Id")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()
                .Identity();
            table
                .WithColumn("ExportId")
                .AsString(100)
                .NotNullable()
                .Unique();

            table
                .WithColumn("Name")
                .AsString(100)
                .NotNullable();
            table
                .WithColumn("Url")
                .AsString(100);
            table
                .WithColumn("Description")
                .AsString(2000);
        }

        public override void Down()
        {
            Delete.Table("Friends");
        }
    }
}