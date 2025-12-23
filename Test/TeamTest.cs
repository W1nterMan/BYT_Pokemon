using System.Reflection;
using Models;

namespace Test
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
            Team team = new Team("Strongers", "Pon", 25, "Super");
            Assert.That(team.Name, Is.EqualTo("Strongers"));
        }

        [Test]
        public void Team_InvalidAttributes()
        {
            Team team = new Team("Sus", "Pon", 25, "Super");

            Assert.Throws<ArgumentException>(() => team.Name = "");
            Assert.Throws<ArgumentException>(() => team.Name = "   ");
        }

        [Test]
        public void Team_Extent()
        {
            new Team("Strongers", "Brock", 25, "Strong");
            new Team("Winners", "Misty", 23, "Win");
            new Team("Supers", "Erika", 30, "Sup");

            var extent = Team.GetTeams();

            Assert.That(extent.Count, Is.EqualTo(3));
            Assert.That(extent.Any(t => t.Name == "Strongers"));
            Assert.That(extent.Any(t => t.Name == "Winners"));
            Assert.That(extent.Any(t => t.Name == "Supers"));
        }

        [Test]
        public void Team_Encapsulation()
        {
            Team team = new Team("Strongers", "Pos", 25, "Strong");

            team.Name = "Winners";

            var extent = Team.GetTeams();
            Assert.That(extent.First().Name, Is.EqualTo("Winners"));
        }

        [Test]
        public void Team_Persistence()
        {
            string path = "team_test.xml";
            if (File.Exists(path)) File.Delete(path);

            new Team("Strongers", "Brock", 25, "Strong");
            new Team("Winners", "Misty", 23, "Win");
            new Team("Supers", "Erika", 30, "Sup");

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
