using DotNetRuServerHipstaMVP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetRuServerHipstaMVP.Infrastructure.Database
{
    public static class DatabaseMappings
    {
        public static void BindSpeaker(this EntityTypeBuilder<Speaker> speaker)
        {
            speaker.ToTable("speakers");

            speaker.Property(x => x.Id).HasColumnName("id");
            speaker.Property(x => x.Name).HasColumnName("name");
            speaker.Property(x => x.CompanyName).HasColumnName("company_name");
            speaker.Property(x => x.CompanyUrl).HasColumnName("company_url");
            speaker.Property(x => x.Description).HasColumnName("description");
            speaker.Property(x => x.BlogUrl).HasColumnName("blog_url");
            speaker.Property(x => x.ContactsUrl).HasColumnName("contacts_url");
            speaker.Property(x => x.TwitterUrl).HasColumnName("twitter_url");
            speaker.Property(x => x.HabrUrl).HasColumnName("habr_url");
            speaker.Property(x => x.GitHubUrl).HasColumnName("github_url");
        }
        
        public static void BindTalk(this EntityTypeBuilder<Talk> talk)
        {
            talk.ToTable("talks");

            talk.Property(x => x.Id).HasColumnName("id");          
            talk.Property(x => x.Title).HasColumnName("title");
            talk.Property(x => x.Description).HasColumnName("description");
            talk.Property(x => x.CodeUrl).HasColumnName("code_url");
            talk.Property(x => x.SlidesUrl).HasColumnName("slides_url");
            talk.Property(x => x.VideoUrl).HasColumnName("video_url");
            talk.Property(x => x.IsDraft).HasColumnName("is_draft");
        }

        public static void BindSpeakerTalk(this EntityTypeBuilder<SpeakerTalk> speakerTalk)
        {
            speakerTalk.ToTable("speaker_talks");
            speakerTalk.HasKey(t => new {t.SpeakerId, t.TalkId});

            speakerTalk.Property(x => x.SpeakerId).HasColumnName("speaker_id");
            speakerTalk.Property(x => x.TalkId).HasColumnName("talk_id");

            speakerTalk.HasOne(x => x.Talk).WithMany(x => x.Speakers).HasForeignKey(x => x.TalkId);
            speakerTalk.HasOne(x => x.Speaker).WithMany(x => x.Talks).HasForeignKey(x => x.SpeakerId);
        }

        public static void BindSeeAlsoTalk(this EntityTypeBuilder<SeeAlsoTalk> seeAlsoTalk)
        {
            seeAlsoTalk.ToTable("see_also_talks");
            
            seeAlsoTalk.HasKey(t => new {t.ParentTalkId, t.ChildTalkId});

            seeAlsoTalk.Property(x => x.ParentTalkId).HasColumnName("parent_talk_id");
            seeAlsoTalk.Property(x => x.ChildTalkId).HasColumnName("child_talk_id");

            seeAlsoTalk.HasOne(x => x.ParentTalk).WithMany(x => x.SeeAlsoTalks).HasForeignKey(x => x.ParentTalkId);
            seeAlsoTalk.HasOne(x => x.ChildTalk).WithMany(x => x.SeeAlsoTalks).HasForeignKey(x => x.ChildTalkId);
        }
    }
}