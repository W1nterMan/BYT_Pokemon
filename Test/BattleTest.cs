using System.Reflection;
using Models;

namespace Test
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
            Battle battle = new Battle("Ongoing", 10, 10,new DateTime(2036, 11, 30, 16, 49, 58, 815),  winner);

            Assert.That(battle.Status, Is.EqualTo("Ongoing"));
            Assert.That(battle.BattleXp, Is.EqualTo(10));
            Assert.That(battle.MoneyIncome, Is.EqualTo(10));
            Assert.That(battle.Time,Is.EqualTo(new DateTime(2036, 11, 30, 16, 49, 58, 815)));
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
            Trainer winner = new Trainer { Name = "Winner" };
            Battle battle = new Battle("Ongoing", 10, 10,DateTime.Now.AddDays(1),  winner);
            var extent = Battle.GetBattles();

            Assert.That(extent.Count, Is.EqualTo(1));
            Assert.That(extent.First().BattleXp, Is.EqualTo(10));
        }
        
        [Test]
        public void Battle_Encapsulation()
        {
            Trainer winner = new Trainer { Name = "Winner" };
            Battle battle = new Battle("Ongoing", 10, 10,DateTime.Now.AddDays(1),  winner);
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

            Battle battle1 = new Battle("Ongoing", 10, 10,DateTime.Now.AddDays(1), null);
            Battle battle2 = new Battle("Finished", 11, 11,DateTime.Now.AddDays(1), null);

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
