using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using DotNetRuServerHipstaMVP.Domain.Entities;
using DotNetRuServerHipstaMVP.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Octokit;

namespace DotNetRuServerHipstaMVP.Importer
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var connectionString = args[0];
            var githubToken = args[1];
            var optionsBuilder = new DbContextOptionsBuilder<DotNetRuServerContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var context = new DotNetRuServerContext(optionsBuilder.Options);

            var github = new GitHubClient(new ProductHeaderValue("DotNetRuServer"));
            var tokenAuth = new Credentials(githubToken);
            github.Credentials = tokenAuth;

            var importer = new ImporterUtils(context, github);
            await importer.ImportCommunities();
            await importer.ImportVenues();
            await importer.ImportFriend();
            await importer.ImportSpeakers();
            await importer.ImportTalks();

//            var meetupsLinks =
//                await github.Repository.Content.GetAllContentsByRef("DotNetRu", "Audit", "db/meetups", "master");
        }
    }

    public class ImporterUtils
    {
        private readonly DotNetRuServerContext _context;
        private readonly GitHubClient _gitHub;
        private readonly HttpClient _httpClient;

        public ImporterUtils(DotNetRuServerContext context, GitHubClient gitHub)
        {
            _context = context;
            _gitHub = gitHub;
            _httpClient = new HttpClient();
        }

        public async Task ImportCommunities()
        {
            var communitiesLinks =
                await _gitHub.Repository.Content.GetAllContentsByRef(
                    "DotNetRu",
                    "Audit",
                    "db/communities",
                    "master"
                );

            foreach (var communityLink in communitiesLinks)
            {
                var responseData = await _httpClient.GetByteArrayAsync(communityLink.DownloadUrl);
                using (var ms = new MemoryStream(responseData))
                {
                    var responseXml = XDocument.Load(ms).Root;
                    var exportId = responseXml?.Element("Id")?.Value;
                    if (string.IsNullOrEmpty(exportId))
                        continue;

                    var existing = await _context.Communities.FirstOrDefaultAsync(x => x.ExportId == exportId.Trim());
                    if (existing != null)
                        continue;

                    _context.Communities.Add(new Community
                    {
                        ExportId = exportId,
                        City = responseXml.Element("City")?.Value,
                        Name = responseXml.Element("Name")?.Value,
                        TimeZone = responseXml.Element("TimeZone")?.Value
                    });
                    _context.SaveChanges();
                }
            }
        }


        public async Task ImportVenues()
        {
            var venuesLinks =
                await _gitHub.Repository.Content.GetAllContentsByRef(
                    "DotNetRu",
                    "Audit",
                    "db/venues",
                    "master"
                );
            foreach (var link in venuesLinks)
            {
                var responseData = await _httpClient.GetByteArrayAsync(link.DownloadUrl);
                using (var ms = new MemoryStream(responseData))
                {
                    var responseXml = XDocument.Load(ms).Root;
                    var exportId = responseXml?.Element("Id")?.Value;
                    if (string.IsNullOrEmpty(exportId))
                        continue;

                    var existing = await _context.Venues.FirstOrDefaultAsync(x => x.ExportId == exportId.Trim());
                    if (existing != null)
                        continue;

                    _context.Venues.Add(new Venue
                    {
                        ExportId = exportId,
                        Name = responseXml.Element("Name")?.Value,
                        MapUrl = responseXml.Element("MapUrl")?.Value,
                        Address = responseXml.Element("Address")?.Value
                    });
                    _context.SaveChanges();
                }
            }
        }

        public async Task ImportFriend()
        {
            var friendsRoots =
                await _gitHub.Repository.Content.GetAllContentsByRef("DotNetRu", "Audit", "db/friends", "master");
            foreach (var friendRoot in friendsRoots)
            {
                var content = await
                    _gitHub.Repository.Content.GetAllContentsByRef(
                        "DotNetRu",
                        "Audit",
                        $"db/friends/{friendRoot.Name}",
                        "master"
                    );
                var index = content.First(x => x.Name == "index.xml");
                var logo = content.First(x => x.Name == "logo.png");
                var smallLogo = content.First(x => x.Name == "logo.small.png");

                var responseData = await _httpClient.GetByteArrayAsync(index.DownloadUrl);
                using (var ms = new MemoryStream(responseData))
                {
                    var responseXml = XDocument.Load(ms).Root;
                    var exportId = responseXml?.Element("Id")?.Value;
                    if (string.IsNullOrEmpty(exportId))
                        continue;

                    var existing = await _context.Friends.FirstOrDefaultAsync(x => x.ExportId == exportId.Trim());
                    if (existing != null)
                        continue;

                    _context.Friends.Add(new Friend
                    {
                        ExportId = exportId,
                        Name = responseXml.Element("Name")?.Value,
                        Url = responseXml.Element("Url")?.Value,
                        Description = responseXml.Element("Description")?.Value,
                        LogoUrl = logo.DownloadUrl,
                        SmallLogoUrl = smallLogo.DownloadUrl
                    });
                    _context.SaveChanges();
                }
            }
        }

        public async Task ImportSpeakers()
        {
            var speakersRoots =
                await _gitHub.Repository.Content.GetAllContentsByRef("DotNetRu", "Audit", "db/speakers", "master");
            foreach (var friendRoot in speakersRoots)
            {
                var content = await
                    _gitHub.Repository.Content.GetAllContentsByRef(
                        "DotNetRu",
                        "Audit",
                        $"db/speakers/{friendRoot.Name}",
                        "master"
                    );
                var index = content.First(x => x.Name == "index.xml");
                var avatar = content.First(x => x.Name == "avatar.jpg");
                var avatarSmall = content.First(x => x.Name == "avatar.small.jpg");

                var responseData = await _httpClient.GetByteArrayAsync(index.DownloadUrl);
                using (var ms = new MemoryStream(responseData))
                {
                    var responseXml = XDocument.Load(ms).Root;
                    var exportId = responseXml?.Element("Id")?.Value;
                    if (string.IsNullOrEmpty(exportId))
                        continue;

                    var existing = await _context.Speakers.FirstOrDefaultAsync(x => x.ExportId == exportId.Trim());
                    if (existing != null)
                        continue;

                    _context.Speakers.Add(new Speaker
                    {
                        ExportId = exportId,
                        Name = responseXml.Element("Name")?.Value,
                        Description = responseXml.Element("Description")?.Value,
                        BlogUrl = responseXml.Element("BlogUrl")?.Value,
                        HabrUrl = responseXml.Element("HabrUrl")?.Value,
                        AvatarUrl = avatar.DownloadUrl,
                        AvatarSmallUrl = avatarSmall.DownloadUrl,
                        CompanyUrl = responseXml.Element("CompanyUrl")?.Value,
                        TwitterUrl = responseXml.Element("TwitterUrl")?.Value,
                        CompanyName = responseXml.Element("CompanyName")?.Value,
                        ContactsUrl = responseXml.Element("ContactsUrl")?.Value,
                        GitHubUrl = responseXml.Element("GitHubUrl")?.Value,
                        IsUserVisible = true
                    });
                    _context.SaveChanges();
                }
            }
        }

        public async Task ImportTalks()
        {
            var talksLinks =
                await _gitHub.Repository.Content.GetAllContentsByRef("DotNetRu", "Audit", "db/talks", "master");
            foreach (var talkLink in talksLinks)
            {
                var responseData = await _httpClient.GetByteArrayAsync(talkLink.DownloadUrl);
                using (var ms = new MemoryStream(responseData))
                {
                    var responseXml = XDocument.Load(ms).Root;
                    var exportId = responseXml?.Element("Id")?.Value;
                    if (string.IsNullOrEmpty(exportId))
                        continue;

                    var existing = await _context.Talks.FirstOrDefaultAsync(x => x.ExportId == exportId.Trim());
                    if (existing != null)
                        continue;

                    var speakerTalkList = new List<SpeakerTalk>();
                    var speakersIds = responseXml.Element("SpeakerIds")?.Descendants();
                    foreach (var id in speakersIds)
                    {
                        var speaker = await _context.Speakers.FirstAsync(x => x.ExportId == id.Value);
                        speakerTalkList.Add(new SpeakerTalk
                        {
                            SpeakerId = speaker.Id
                        });
                    }

                    _context.Talks.Add(new Talk
                    {
                        ExportId = exportId,
                        Title = responseXml.Element("Title")?.Value,
                        Description = responseXml.Element("Description")?.Value,
                        SlidesUrl = responseXml.Element("SlidesUrl")?.Value,
                        CodeUrl = responseXml.Element("CodeUrl")?.Value,
                        VideoUrl = responseXml.Element("VideoUrl")?.Value,
                        Speakers = speakerTalkList,
                        IsDraft = false,
                        IsUserVisible = true
                    });
                    _context.SaveChanges();
                }
            }
        }
    }
}