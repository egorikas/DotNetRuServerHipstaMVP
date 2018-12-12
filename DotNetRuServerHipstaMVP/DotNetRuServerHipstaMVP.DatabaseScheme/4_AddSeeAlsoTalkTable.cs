using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(4)]
    public class AddSeeAlsoTalkTable : Migration
    {
        public override void Up()
        {
            var table = Create.Table("see_also_talks");
            
            table
                .WithColumn("parent_talk_id")
                .AsString(100)
                .NotNullable();            
            table
                .WithColumn("child_talk_id")
                .AsString(100)
                .NotNullable();

            Create.PrimaryKey("id")
                .OnTable("see_also_talks")
                .Columns("parent_talk_id", "child_talk_id");

        }

        public override void Down()
        {
            Delete.Table("see_also_talks");
        }
    }
}