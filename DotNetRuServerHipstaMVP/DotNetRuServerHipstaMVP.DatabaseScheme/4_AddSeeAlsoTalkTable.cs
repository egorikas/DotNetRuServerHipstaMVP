using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(4)]
    public class AddSeeAlsoTalkTable : Migration
    {
        public override void Up()
        {
            var table = Create.Table("SeeAlsoTalks");

            table
                .WithColumn("ParentTalkId")
                .AsInt32()
                .NotNullable();
            table
                .WithColumn("ChildTalkId")
                .AsInt32()
                .NotNullable();

            Create.PrimaryKey("SeeAlsoTalksId")
                .OnTable("SeeAlsoTalks")
                .Columns("ParentTalkId", "ChildTalkId");
        }

        public override void Down()
        {
            Delete.Table("SeeAlsoTalks");
        }
    }
}