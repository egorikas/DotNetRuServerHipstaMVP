using System.Threading.Tasks;
using Octokit;

namespace DotNetRuServerHipstaMVP.Importer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var github = new GitHubClient(new ProductHeaderValue("DotNetRuServer"));
            var communitiesFilesLinks =
                await github.Repository.Content.GetAllContentsByRef("DotNetRu", "Audit", "db/communities", "master");
            var venuesLinks =
                await github.Repository.Content.GetAllContentsByRef("DotNetRu", "Audit", "db/venues", "master");
            var friendsLinks =
                await github.Repository.Content.GetAllContentsByRef("DotNetRu", "Audit", "db/friends", "master");
            var talksLinks =
                await github.Repository.Content.GetAllContentsByRef("DotNetRu", "Audit", "db/talks", "master");
            var speakersLinks =
                await github.Repository.Content.GetAllContentsByRef("DotNetRu", "Audit", "db/speakers", "master");
            var meetupsLinks =
                await github.Repository.Content.GetAllContentsByRef("DotNetRu", "Audit", "db/meetups", "master");
        }
    }
}