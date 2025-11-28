using System.Reflection;
using TestProject6.BYT_Pokemon.Models;

namespace TestProject6.BYT_Pokemon.Test
{
    public class TeamTest
    {
        [SetUp]
        public void Setup()
        {
            var field = typeof(Team).GetField("_extent", BindingFlags.Static | BindingFlags.NonPublic);
            field.SetValue(null, new List<Team>());
        }

        [Test]
        public void Team_Attributes()
        {
            Team team = new Team("Strongers");
            Assert.That(team.Name, Is.EqualTo("Strongers"));
        }

        [Test]
        public void Team_InvalidAttributes()
        {
            Team team = new Team();
            Assert.Throws<ArgumentException>(() => team.Name = "");
            Assert.Throws<ArgumentException>(() => team.Name = "   ");
        }

        [Test]
        public void Team_Extent()
        {
            Team team1 = new Team("Strongers");
            Team team2 = new Team("Winners");
            Team team3 = new Team("Supers");

            var extent = Team.GetTeams();
            Assert.That(extent.Count, Is.EqualTo(3));
            Assert.That(extent.Any(t => t.Name == "Strongers"));
            Assert.That(extent.Any(t => t.Name == "Winners"));
            Assert.That(extent.Any(t => t.Name == "Supers"));
        }

        [Test]
        public void Team_Encapsulation()
        {
            Team team = new Team("Strongers");

            var extent = Team.GetTeams();
            team.Name = "Winners";

            var updatedExtent = Team.GetTeams();
            Assert.That(updatedExtent.First().Name, Is.EqualTo("Winners"));
        }

        [Test]
        public void Team_Persistence()
        {
            string path = "team_test.xml";
            if (File.Exists(path)) File.Delete(path);

            Team team1 = new Team("Strongers");
            Team team2 = new Team("Winners");
            Team team3 = new Team("Supers");

            Team.Save(path);

            var field = typeof(Team).GetField("_extent", BindingFlags.Static | BindingFlags.NonPublic);
            field.SetValue(null, new List<Team>());

            bool loadSuccess = Team.Load(path);
            Assert.IsTrue(loadSuccess);

            var loadedExtent = Team.GetTeams();
            Assert.That(loadedExtent.Count, Is.EqualTo(3));
            Assert.That(loadedExtent.Any(t => t.Name == "Strongers"));
            Assert.That(loadedExtent.Any(t => t.Name == "Winners"));
            Assert.That(loadedExtent.Any(t => t.Name == "Supers"));
        }
    }
}
