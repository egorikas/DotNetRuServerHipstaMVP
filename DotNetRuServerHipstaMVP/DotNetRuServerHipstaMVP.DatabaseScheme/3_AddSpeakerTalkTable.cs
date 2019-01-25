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
                .AsInt32()
                .NotNullable();
            table
                .WithColumn("TalkId")
                .AsInt32()
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