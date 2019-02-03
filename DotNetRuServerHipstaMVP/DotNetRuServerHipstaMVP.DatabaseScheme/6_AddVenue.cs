using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(6)]
    public class AddVenue : Migration
    {
        public override void Up()
        {
            var table = Create.Table("Venues");
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
                .WithColumn("MapUrl")
                .AsString(200);
        }

        public override void Down()
        {
            Delete.Table("Venues");
        }
    }
}