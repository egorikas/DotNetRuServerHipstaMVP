using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(3)]
    public class AddSpeakerTalkTable : Migration
    {
        public override void Up()
        {
            var table = Create.Table("speaker_talks");
            
            table
                .WithColumn("speaker_id")
                .AsString(100)
                .NotNullable();            
            table
                .WithColumn("talk_id")
                .AsString(100)
                .NotNullable();

            Create.PrimaryKey("id")
                .OnTable("speaker_talks")
                .Columns("speaker_id", "talk_id");

        }

        public override void Down()
        {
            Delete.Table("speaker_talks");
        }
    }
}