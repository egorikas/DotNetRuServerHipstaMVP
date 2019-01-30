using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    public class AddCommunity : Migration
    {
        public override void Up()
        {
            var table = Create.Table("Communities");
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
                .WithColumn("City")
                .AsString(50);
            table
                .WithColumn("TimeZone")
                .AsString(50);
        }

        public override void Down()
        {
            Delete.Table("Communities");
        }
    }
}