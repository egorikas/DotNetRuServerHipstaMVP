using DotNetRuServerHipstaMVP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetRuServerHipstaMVP.Infrastructure.Database
{
    public static class DatabaseMappings
    {
        public static void BindSpeaker(this EntityTypeBuilder<Speaker> speaker)
        {
            speaker.ToTable("Speakers");

            speaker.Property(x => x.Id).HasColumnName("Id");
            speaker.Property(x => x.Name).HasColumnName("Name");
            speaker.Property(x => x.CompanyName).HasColumnName("CompanyName");
            speaker.Property(x => x.CompanyUrl).HasColumnName("CompanyUrl");
            speaker.Property(x => x.Description).HasColumnName("Description");
            speaker.Property(x => x.BlogUrl).HasColumnName("BlogUrl");
            speaker.Property(x => x.ContactsUrl).HasColumnName("ContactsUrl");
            speaker.Property(x => x.TwitterUrl).HasColumnName("TwitterUrl");
            speaker.Property(x => x.HabrUrl).HasColumnName("HabrUrl");
            speaker.Property(x => x.GitHubUrl).HasColumnName("GitHubUrl");
        }
        
        public static void BindTalk(this EntityTypeBuilder<Talk> talk)
        {
            talk.ToTable("Talks");

            talk.Property(x => x.Id).HasColumnName("Id");          
            talk.Property(x => x.Title).HasColumnName("Title");
            talk.Property(x => x.Description).HasColumnName("Description");
            talk.Property(x => x.CodeUrl).HasColumnName("CodeUrl");
            talk.Property(x => x.SlidesUrl).HasColumnName("SlidesUrl");
            talk.Property(x => x.VideoUrl).HasColumnName("VideoUrl");
            talk.Property(x => x.IsDraft).HasColumnName("IsDraft");
        }

        public static void BindSpeakerTalk(this EntityTypeBuilder<SpeakerTalk> speakerTalk)
        {
            speakerTalk.ToTable("SpeakerTalks");
            speakerTalk.HasKey(t => new {t.SpeakerId, t.TalkId});

            speakerTalk.Property(x => x.SpeakerId).HasColumnName("SpeakerId");
            speakerTalk.Property(x => x.TalkId).HasColumnName("TalkId");

            speakerTalk.HasOne(x => x.Talk).WithMany(x => x.Speakers).HasForeignKey(x => x.TalkId);
            speakerTalk.HasOne(x => x.Speaker).WithMany(x => x.Talks).HasForeignKey(x => x.SpeakerId);
        }

        public static void BindSeeAlsoTalk(this EntityTypeBuilder<SeeAlsoTalk> seeAlsoTalk)
        {
            seeAlsoTalk.ToTable("SeeAlsoTalks");
            
            seeAlsoTalk.HasKey(t => new {t.ParentTalkId, t.ChildTalkId});

            seeAlsoTalk.Property(x => x.ParentTalkId).HasColumnName("ParentTalkId");
            seeAlsoTalk.Property(x => x.ChildTalkId).HasColumnName("ChildTalkId");

            seeAlsoTalk.HasOne(x => x.ParentTalk).WithMany(x => x.SeeAlsoTalks).HasForeignKey(x => x.ParentTalkId);
            seeAlsoTalk.HasOne(x => x.ChildTalk).WithMany(x => x.SeeAlsoTalks).HasForeignKey(x => x.ChildTalkId);
        }
    }
}