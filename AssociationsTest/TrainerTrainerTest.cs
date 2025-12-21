using Models;

namespace AssociationsTest;

public class TrainerTrainerTest
{
    [Test]
    public void TrainerTrainerChallenge()
    {
        Trainer bubba = new Trainer(1, 1000, [], "Active", "Bubba", 55);
        Trainer hubba = new Trainer(2, 800, [], "Active", "Hubba", 65);

        bubba.ChallengeTrainer(hubba);

        Assert.That(bubba.ChallangeTrainer, Is.EqualTo(hubba));
        Assert.That(bubba.ChallangeTrainer, Is.EqualTo(hubba));
    }

    [Test]
    public void MultyChallangeTrainer()
    {
        Trainer bubba = new Trainer(1, 1000, [], "Active", "Bubba", 55);
        Trainer hubba = new Trainer(2, 800, [], "Active", "Hubba", 65);
        Trainer asd = new Trainer(3, 900, [], "Active", "asd", 88);

        asd.ChallengeTrainer(hubba);

        Assert.Throws<InvalidOperationException>(() =>
        {
            asd.ChallengeTrainer(hubba);
        });
    }

    [Test]
    public void StopChallenge()
    {
        Trainer bubba = new Trainer(1, 1000, [], "Active", "Bubba", 55);
        Trainer hubba = new Trainer(2, 800, [], "Active", "Hubba", 65);

        bubba.ChallengeTrainer(hubba);
        bubba.StopChallengeTrainer();

        Assert.IsNull(bubba.ChallangeTrainer);
        Assert.IsNull(hubba.ChallangeTrainer);
    }
}