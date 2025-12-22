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

        private Trainer CreateTrainer(int id, string name)
        {
            return new Trainer(id, 100,[], "Active", name, 20);
        }

        [Test]
        public void Battle_Attributes()
        {
            var t1 = CreateTrainer(1, "Hubba");
            var t2 = CreateTrainer(2, "Bubba");

            Battle battle = new Battle("Ongoing", 10, 10, new DateTime(2036, 11, 30, 16, 49, 58, 815), t1, t1, t2);

            Assert.That(battle.Status, Is.EqualTo("Ongoing"));
            Assert.That(battle.BattleXp, Is.EqualTo(10));
            Assert.That(battle.MoneyIncome, Is.EqualTo(10));
            Assert.That(battle.Time, Is.EqualTo(new DateTime(2036, 11, 30, 16, 49, 58, 815)));
            Assert.That(battle.Winner, Is.EqualTo(t1));
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
            var t1 = CreateTrainer(1, "Hubba");
            var t2 = CreateTrainer(2, "Bubba");

            new Battle("Ongoing", 10, 10, DateTime.Now.AddDays(1), null, t1, t2);

            var extent = Battle.GetBattles();

            Assert.That(extent.Count, Is.EqualTo(1));
            Assert.That(extent.First().BattleXp, Is.EqualTo(10));
        }

        [Test]
        public void Battle_Encapsulation()
        {
            var t1 = CreateTrainer(1, "Hubba");
            var t2 = CreateTrainer(2, "Bubba");

            Battle battle = new Battle("Ongoing", 10, 10, DateTime.Now.AddDays(1), null, t1, t2);

            Assert.That(Battle.GetBattles().First().BattleXp, Is.EqualTo(10));

            battle.BattleXp = 11;
            battle.Status = "Finished";
            battle.MoneyIncome = 11;

            var updated = Battle.GetBattles().First();

            Assert.That(updated.BattleXp, Is.EqualTo(11));
            Assert.That(updated.Status, Is.EqualTo("Finished"));
            Assert.That(updated.MoneyIncome, Is.EqualTo(11));
        }

        [Test]
        public void Battle_Persistence()
        {
            string path = "battle_test.xml";
            if (File.Exists(path)) File.Delete(path);

            var t1 = CreateTrainer(1, "Hubba");
            var t2 = CreateTrainer(2, "Bubba");
            var t3 = CreateTrainer(3, "Helena");
            var t4 = CreateTrainer(4, "H");

            Battle battle1 = new Battle("Ongoing", 10, 10, DateTime.Now.AddDays(1), null, t1, t2);

            Battle battle2 = new Battle("Finished", 11, 11, DateTime.Now.AddDays(2), null, t3, t4);

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
