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
                .AsString(100)
                .NotNullable();            
            table
                .WithColumn("ChildTalkId")
                .AsString(100)
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