using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(3)]
    public class AddSpeakerTalkTable : Migration
    {
        public override void Up()
        {
            var table = Create.Table("SpeakerTalks");

            table
                .WithColumn("SpeakerId")
                .AsString(100)
                .NotNullable();
            table
                .WithColumn("TalkId")
                .AsString(100)
                .NotNullable();

            Create.PrimaryKey("SpeakerTalksId")
                .OnTable("SpeakerTalks")
                .Columns("SpeakerId", "TalkId");
        }

        public override void Down()
        {
            Delete.Table("SpeakerTalks");
        }
    }
}