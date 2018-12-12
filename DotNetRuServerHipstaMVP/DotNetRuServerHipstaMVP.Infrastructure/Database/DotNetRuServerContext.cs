using DotNetRuServerHipstaMVP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetRuServerHipstaMVP.Infrastructure.Database
{
    public class DotNetRuServerContext : DbContext
    {
        public DotNetRuServerContext(DbContextOptions options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Speaker>().BindSpeaker();
            modelBuilder.Entity<Talk>().BindTalk();
            
            modelBuilder.Entity<SeeAlsoTalk>().BindSeeAlsoTalk();
            modelBuilder.Entity<SpeakerTalk>().BindSpeakerTalk();          
        }
        
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Talk> Talks { get; set; }
        
        // M2M-tables
        public DbSet<SeeAlsoTalk> SeeAlsoTalks { get; set; }
        public DbSet<SpeakerTalk> SpeakerTalks { get; set; }

    }
}