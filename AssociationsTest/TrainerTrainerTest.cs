using System.Runtime.InteropServices.ComTypes;
using Models;

namespace AssociationsTest;

public class TrainerTrainerTest
{
    [Test]
    public void TrainerTrainerChallenge()
    {
        Trainer bubba = new Trainer(1, 1000, [], "Active", "Bubba", 55);
        Trainer hubba = new Trainer(2, 800, [], "Active", "Hubba", 65);

        Battle battle = new Battle("Ongoing", 123, 123, DateTime.Now.AddMinutes(1), null,bubba, hubba );

        Assert.That(bubba.Battles.Contains(battle), Is.True);
        Assert.That(hubba.Battles.Contains(battle), Is.True);
    }

    [Test]
    public void MultyChallangeTrainer()
    {
        Trainer bubba = new Trainer(1, 1000, [], "Active", "Bubba", 55);
        Trainer hubba = new Trainer(2, 800, [], "Active", "Hubba", 65);
        Trainer asd = new Trainer(3, 900, [], "Active", "asd", 88);

        _ = new Battle("Ongoing", 123, 123, DateTime.Now.AddMinutes(1), null, bubba, hubba);
        
        Assert.Throws<InvalidOperationException>(() =>
        {
            new Battle("Ongoing", 123, 123, DateTime.Now.AddMinutes(1), null, asd, hubba);
        });
    }

    [Test]
    public void RemoveBattle()
    {
        Trainer bubba = new Trainer(1, 1000, [], "Active", "Bubba", 55);
        Trainer hubba = new Trainer(2, 800, [], "Active", "Hubba", 65);

        Battle battle = new Battle("Finished", 123, 123, DateTime.Now.AddMinutes(1), bubba, bubba, hubba);
        
        battle.RemoveBattle();

        Assert.That(bubba.Battles.Count, Is.EqualTo(0));
        Assert.That(hubba.Battles.Count, Is.EqualTo(0));
    }
}