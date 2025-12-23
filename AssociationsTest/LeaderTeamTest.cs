using Models;

namespace AssociationsTest;

public class LeaderTeamTest
{
    [Test]
    public void LeaderTeamComposition()
    {
        Team team = new Team("Red", "Bubba", 55, "Boo");

        Assert.IsNotNull(team.Leader);
        Assert.That(team.Leader.Team, Is.EqualTo(team));
        Assert.That(team.Leader.SpecialPrefix, Is.EqualTo("Boo"));
    }
    
}