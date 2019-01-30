using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    public class AddFriend : Migration
    {
        public override void Up()
        {
            var table = Create.Table("Friends");
            table
                .WithColumn("Id")
                .AsInt32()
                .NotNullable()
                .PrimaryKey();
            table
                .WithColumn("ExportId")
                .AsString(100)
                .NotNullable()
                .PrimaryKey();

            table
                .WithColumn("Name")
                .AsString(100)
                .NotNullable();
            table
                .WithColumn("Url")
                .AsString(100);
            table
                .WithColumn("Description")
                .AsString(500);
        }

        public override void Down()
        {
            Delete.Table("Friends");
        }
    }
}