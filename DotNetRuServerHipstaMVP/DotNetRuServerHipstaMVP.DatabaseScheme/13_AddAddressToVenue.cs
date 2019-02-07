using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(13)]
    public class AddAddressToVenue : Migration
    {
        public override void Up()
        {
            Create
                .Column("Address")
                .OnTable("Venues")
                .AsString(150)
                .NotNullable();
        }

        public override void Down()
        {
            Delete.Column("Address").FromTable("Venues");
        }
    }
}