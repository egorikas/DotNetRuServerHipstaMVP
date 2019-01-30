using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    public class AddMeetup  : Migration
    {
        public override void Up()
        {
            var table = Create.Table("Meetups");
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
                .WithColumn("VenueId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("Venues", "Id");

            table
                .WithColumn("CommunityId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("Communities", "Id");
        }

        public override void Down()
        {
            Delete.Table("Meetups");
        }
    }
}