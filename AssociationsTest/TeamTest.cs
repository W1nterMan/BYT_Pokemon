using Models;

namespace AssociationsTest;

public class TeamTest
{
    [Test]
    public void AddTrainerTest()
    {
        Team team = new Team("Team Red");

        Trainer trainer = new Trainer(123, 1234, new string[0], "Active", "Hanna", 32);

        team.AddTeamMember(trainer);

        Assert.That(team.GetTeamMembers().Count, Is.EqualTo(1));
        Assert.That(team.GetTeamMembers().ContainsKey(123));
        Assert.That(trainer.Team, Is.EqualTo(team));
    }

    [Test]
    public void RemoveTrainerTest()
    {
        Team team = new Team("Team Red");

        Trainer trainer = new Trainer(1234, 10344, new string[0], "Active", "Maria", 3);

        team.AddTeamMember(trainer);
        team.RemoveTeamMember(1234);

        Assert.That(team.GetTeamMembers().Count, Is.EqualTo(0));
        Assert.IsNull(trainer.Team);
    }

    [Test]
    public void TrainerInTwoTeamsTest()
    {
        Team teamRed = new Team("Team Red");
        Team teamBlue = new Team("Team Blue");

        Trainer trainer = new Trainer(12, 1000, new string[0], "Active", "Bubba", 41);

        teamRed.AddTeamMember(trainer);

        Assert.Throws<InvalidOperationException>(() =>
            teamBlue.AddTeamMember(trainer)
        );
    }
    
    [Test]
    public void DeleteTeamTest()
    {
        Team teamRed = new Team("Team Red");

        Trainer trainer = new Trainer(12, 1000, new string[0], "Active", "Bubba", 41);

        teamRed.AddTeamMember(trainer);

        teamRed.DeleteTeam();
        
        Assert.That(trainer.Team,Is.Null);
    }
}