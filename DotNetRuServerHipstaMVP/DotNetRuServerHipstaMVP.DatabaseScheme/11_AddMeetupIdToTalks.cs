using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    public class AddMeetupIdToTalks : Migration
    {
        public override void Up()
        {
            Create
                .Column("MeetupID")
                .OnTable("Talks")
                .AsInt32()
                .Nullable()
                .ForeignKey("Meetups", "Id");
        }

        public override void Down()
        {
            Delete.Column("MeetupID").FromTable("Talks");
        }
    }
}