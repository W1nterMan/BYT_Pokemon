using System.Runtime.InteropServices.ComTypes;
using Models;

namespace AssociationsTest;

public class TrainerTrainerTest
{
    [Test]
    public void TrainerTrainerChallenge()
    {
        Trainer bubba = new PersonBuilder("Bubba", 55).AsTrainer(1, 1000, [], "Active").Build().Trainer!;
        Trainer hubba = new PersonBuilder("Hubba", 65).AsTrainer(2, 800, [], "Active").Build().Trainer!;

        Battle battle = new Battle("Ongoing", 123, 123, DateTime.Now.AddMinutes(1), null,bubba, hubba );

        Assert.That(bubba.Battles.Contains(battle), Is.True);
        Assert.That(hubba.Battles.Contains(battle), Is.True);
    }

    [Test]
    public void MultyChallangeTrainer()
    {
        Trainer bubba = new PersonBuilder("Bubba", 55).AsTrainer(1, 1000, [], "Active").Build().Trainer!;

        Trainer hubba = new PersonBuilder("Hubba", 65).AsTrainer(2, 800, [], "Active").Build().Trainer!;

        Trainer asd = new PersonBuilder("asd", 88).AsTrainer(3, 900, [], "Active").Build().Trainer!;

        _ = new Battle("Ongoing", 123, 123, DateTime.Now.AddMinutes(1), null, bubba, hubba);
        
        Assert.Throws<InvalidOperationException>(() =>
        {
            new Battle("Ongoing", 123, 123, DateTime.Now.AddMinutes(1), null, asd, hubba);
        });
    }

    [Test]
    public void RemoveBattle()
    {
        Trainer bubba = new PersonBuilder("Bubba", 55).AsTrainer(1, 1000, [], "Active").Build().Trainer!;
        Trainer hubba = new PersonBuilder("Hubba", 65).AsTrainer(2, 800, [], "Active").Build().Trainer!;

        Battle battle = new Battle("Finished", 123, 123, DateTime.Now.AddMinutes(1), bubba, bubba, hubba);
        
        battle.RemoveBattle();

        Assert.That(bubba.Battles.Count, Is.EqualTo(0));
        Assert.That(hubba.Battles.Count, Is.EqualTo(0));
    }
}