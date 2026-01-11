using Models;

namespace AssociationsTest;

public class TeamTest
{
    [Test]
    public void AddTrainerTest()
    {
        Team team = new Team();

        Trainer trainer = new PersonBuilder("Hanna", 32).AsTrainer(123, 1234, new string[0], "Active").Build().Trainer!;

        team.AddTeamMember(trainer);

        Assert.That(team.GetTeamMembers().Count, Is.EqualTo(1));
        Assert.That(team.GetTeamMembers().ContainsKey(123));
        Assert.That(trainer.Team, Is.EqualTo(team));
    }

    [Test]
    public void RemoveTrainerTest()
    {
        Team team = new Team();
        Trainer trainer = new PersonBuilder("Maria", 3).AsTrainer(1234, 10344, new string[0], "Active").Build().Trainer!;

        team.AddTeamMember(trainer);
        team.RemoveTeamMember(1234);

        Assert.That(team.GetTeamMembers().Count, Is.EqualTo(0));
        Assert.IsNull(trainer.Team);
    }

    [Test]
    public void TrainerInTwoTeamsTest()
    {
        Team teamRed = new Team();
        Team teamBlue = new Team();
        Trainer trainer = new PersonBuilder("Bubba", 41).AsTrainer(12, 1000, new string[0], "Active").Build().Trainer!;

        teamRed.AddTeamMember(trainer);

        Assert.Throws<InvalidOperationException>(() =>
            teamBlue.AddTeamMember(trainer)
        );
    }
    
    [Test]
    public void DeleteTeamTest()
    {
        Team teamRed = new Team();

        Trainer trainer = new PersonBuilder("Bubba", 41).AsTrainer(12, 1000, new string[0], "Active").Build().Trainer!;

        teamRed.AddTeamMember(trainer);

        teamRed.DeleteTeam();
        
        Assert.That(trainer.Team,Is.Null);
    }
}