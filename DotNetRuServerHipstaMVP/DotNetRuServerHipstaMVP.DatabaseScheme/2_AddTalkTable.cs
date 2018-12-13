using FluentMigrator;

namespace DotNetRuServerHipstaMVP.DatabaseScheme
{
    [Migration(2)]
    public class AddTalkTable  : Migration
    {
        public override void Up()
        {
            var table = Create.Table("Talks");
            table
                .WithColumn("Id")
                .AsString(100)
                .NotNullable()
                .PrimaryKey();
            
            table
                .WithColumn("Title")
                .AsString(100)
                .NotNullable();            
            table
                .WithColumn("Description")
                .AsString(100);         
            table
                .WithColumn("CodeUrl")
                .AsString(100); 
            table
                .WithColumn("SlidesUrl")
                .AsString(100);
            table
                .WithColumn("VideoUrl")
                .AsString(100);
            table
                .WithColumn("IsDraft")
                .AsBoolean()
                .WithDefaultValue(true);
        }

        public override void Down()
        {
            Delete.Table("Talks");
        }
    }
}