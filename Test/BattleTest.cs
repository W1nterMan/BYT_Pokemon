using System.Reflection;
using TestProject6.BYT_Pokemon.Models;

namespace TestProject6.BYT_Pokemon.Test
{
    public class BattleTest
    {
        [SetUp]
        public void Setup()
        {
            var field = typeof(Battle).GetField("_extent", BindingFlags.Static | BindingFlags.NonPublic);
            field.SetValue(null, new List<Battle>());
        }

        [Test]
        public void Battle_Attributes()
        {
            Trainer winner = new Trainer { Name = "Winner" };
            Battle battle = new Battle("Ongoing", 10, 10, winner);

            Assert.That(battle.Status, Is.EqualTo("Ongoing"));
            Assert.That(battle.BattleXp, Is.EqualTo(10));
            Assert.That(battle.MoneyIncome, Is.EqualTo(10));
            Assert.That(battle.Winner, Is.EqualTo(winner));
        }

        [Test]
        public void Battle_InvalidAttributes()
        {
            Battle battle = new Battle();
            Assert.Throws<ArgumentOutOfRangeException>(() => battle.BattleXp = -1);
            Assert.Throws<ArgumentOutOfRangeException>(() => battle.MoneyIncome = -1);
            Assert.Throws<ArgumentException>(() => battle.Status = "September");
        }

        [Test]
        public void Battle_Extent()
        {
            Battle battle = new Battle("Ongoing", 10, 10, null);
            var extent = Battle.GetBattles();

            Assert.That(extent.Count, Is.EqualTo(1));
            Assert.That(extent.First().BattleXp, Is.EqualTo(10));
        }
        
        [Test]
        public void Battle_Encapsulation()
        {
            Battle battle = new Battle("Ongoing", 10, 10, null);
            var extent = Battle.GetBattles();

            Assert.That(extent.First().BattleXp, Is.EqualTo(10));

            battle.BattleXp = 11;
            battle.Status = "Finished";
            battle.MoneyIncome = 11;

            var updatedExtent = Battle.GetBattles();
            Assert.That(updatedExtent.First().BattleXp, Is.EqualTo(11));
            Assert.That(updatedExtent.First().Status, Is.EqualTo("Finished"));
            Assert.That(updatedExtent.First().MoneyIncome, Is.EqualTo(11));
        }


        [Test]
        public void Battle_Persistence()
        {
            string path = "battle_test.xml";
            if (File.Exists(path)) File.Delete(path);

            Battle battle1 = new Battle("Ongoing", 10, 10, null);
            Battle battle2 = new Battle("Finished", 11, 11, null);

            Battle.Save(path);

            var field = typeof(Battle).GetField("_extent", BindingFlags.Static | BindingFlags.NonPublic);
            field.SetValue(null, new List<Battle>());

            bool loadSuccess = Battle.Load(path);
            Assert.IsTrue(loadSuccess);

            var loadedExtent = Battle.GetBattles();
            Assert.That(loadedExtent.Count, Is.EqualTo(2));
            Assert.That(loadedExtent.Any(b => b.Status == "Ongoing"));
            Assert.That(loadedExtent.Any(b => b.Status == "Finished"));
        }
    }
}
