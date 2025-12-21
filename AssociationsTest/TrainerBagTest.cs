using Models;

namespace AssociationsTest;

public class TrainerBagTest
{
    [Test]
    public void TrainerBagComposition()
    {
        Trainer bubba = new Trainer(1, 1000, [], "Active", "Bubba", 55);

        Assert.IsNotNull(bubba.Bag);
        Assert.That(bubba.Bag.Owner, Is.EqualTo(bubba));
    }

    [Test]
    public void BagWithoutTrainer()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Bag(null);
        });
    }
}