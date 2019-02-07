using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(11)]
    public class AddMeetupIdToTalks : Migration
    {
        public override void Up()
        {
            Create
                .Column("MeetupId")
                .OnTable("Talks")
                .AsInt32()
                .Nullable()
                .ForeignKey("Meetups", "Id");
        }

        public override void Down()
        {
            Delete.Column("MeetupId").FromTable("Talks");
        }
    }
}