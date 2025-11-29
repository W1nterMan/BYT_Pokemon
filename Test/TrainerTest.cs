using System.Reflection;
using Models;

namespace Test
{
    public class TrainerTest
    {
        [SetUp]
        public void Setup()
        {
            var field = typeof(Trainer).GetField("_extent", BindingFlags.Static | BindingFlags.NonPublic);
            field.SetValue(null, new List<Trainer>());
        }

        [Test]
        public void Trainer_Attributes()
        {
            var badges = new String[] { "Indigo", "Zephyr" };

            Trainer trainer = new Trainer
            {
                TotalMoney = 23040,
                Badges = badges,
                Status = nameof(TrainerStatus.Active),
                Name = "Ivan",
                Age = 29
            };

            Assert.That(trainer.TotalMoney, Is.EqualTo(23040));
            Assert.That(trainer.Badges.Length, Is.EqualTo(2));
            Assert.That(trainer.Status, Is.EqualTo(nameof(TrainerStatus.Active)));
            Assert.That(trainer.Name, Is.EqualTo("Ivan"));
            Assert.That(trainer.Age, Is.EqualTo(29));
        }

        [Test]
        public void Trainer_InvalidAttributes()
        {
            var trainer = new Trainer();

            Assert.Throws<ArgumentOutOfRangeException>(() => trainer.TotalMoney = -1);
            Assert.Throws<ArgumentNullException>(() => trainer.Badges = null);
            Assert.Throws<ArgumentException>(() => trainer.Badges = new String[] { null! });
            Assert.Throws<ArgumentException>(() => trainer.Status = "Hello");
            Assert.Throws<ArgumentException>(() => trainer.Name = "");
            Assert.Throws<ArgumentOutOfRangeException>(() => trainer.Age = -1);
        }

        // [Test]
        // public void Trainer_Extent()
        // {
        //     Trainer ivan1 = new Trainer { Name = "Ivan1" };
        //     
        //     var extent = Trainer.GetTrainers();
        //     Assert.That(extent.Count, Is.EqualTo(1));
        //     Assert.That(extent.First().Name, Is.EqualTo("Ivan1"));
        // }

        // [Test]
        // public void Trainer_Encapsulation()
        // {
        //     Trainer trainer = new Trainer { Name = "Ivan2" };
        //
        //     var extent = Trainer.GetTrainers();
        //     trainer.Name = "Ivan22";
        //
        //     var updatedExtent = Trainer.GetTrainers();
        //     Assert.That(updatedExtent.First().Name, Is.EqualTo("Ivan22"));
        // }

        // [Test]
        // public void Trainer_Persistence()
        // {
        //     string path = "trainer_test.xml";
        //     if (File.Exists(path)) File.Delete(path);
        //
        //     Trainer ivan = new Trainer { Name = "Ivan" };
        //     Trainer ivan2 = new Trainer { Name = "Ivan2" };
        //
        //     Trainer.Save(path);
        //     var field = typeof(Trainer).GetField("_extent", BindingFlags.Static | BindingFlags.NonPublic);
        //     field.SetValue(null, new List<Trainer>());
        //     bool loadSuccess = Trainer.Load(path);
        //     Assert.IsTrue(loadSuccess, "Load should return true");
        //     var loadedExtent = Trainer.GetTrainers();
        //     Assert.That(loadedExtent.Count, Is.EqualTo(2));
        //     Assert.That(loadedExtent.Any(t => t.Name == "Ivan"), Is.True);
        //     Assert.That(loadedExtent.Any(t => t.Name == "Ivan2"), Is.True);
        // }
    }
}
