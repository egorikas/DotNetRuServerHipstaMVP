using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(2)]
    public class AddTalkTable  : Migration
    {
        public override void Up()
        {
            var table = Create.Table("talks");
            table
                .WithColumn("id")
                .AsString(100)
                .NotNullable()
                .PrimaryKey();
            
            table
                .WithColumn("title")
                .AsString(100)
                .NotNullable();            
            table
                .WithColumn("description")
                .AsString(100);         
            table
                .WithColumn("code_url")
                .AsString(100); 
            table
                .WithColumn("slides_url")
                .AsString(100);
            table
                .WithColumn("video_url")
                .AsString(100);
            table
                .WithColumn("is_draft")
                .AsBoolean()
                .WithDefaultValue(true);
        }

        public override void Down()
        {
            Delete.Table("talks");
        }
    }
}