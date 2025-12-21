using Models;

namespace AssociationsTest;

public class LeaderTeamTest
{
    [Test]
    public void LeaderTeamComposition()
    {
        Leader leader = new Leader("Bubba", 55, "Boo");
        Team team = new Team("Red", leader);

        Assert.That(leader.Team, Is.EqualTo(team));
        Assert.That(team.Leader, Is.EqualTo(leader));
    }

    [Test]
    public void TeamWithoutLeader()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Team("Invalid Team", null);
        });
    }
}