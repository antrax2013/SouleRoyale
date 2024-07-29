using Xunit.Gherkin.Quick;
using NFluent;

namespace SouleRoyale.Tests.TestCases;

[FeatureFile(@"./Features/Team.feature")]
public sealed class TeamTests : Feature
{
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
    private Team _team;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

    [Given(@"I ask to create a new team")]
    public void I_ask_to_create_a_new_player() { }

    [When(@"I create a new team")]
    public void I_create_a_new_player()
    {
        _team = new Team();
    }

    [Then(@"The team have 11 players")]
    public void The_result_should_be_z_on_the_screen()
    {
        Check.That(_team.Players).CountIs(11);
    }

    [And(@"All players have 4 life points")]
    public void All_players_have_4_life_points()
    {
        var playersWith4LifePoints = _team.Players.Where(p => p.LifePoints == 4).ToList();
        Check.That(playersWith4LifePoints).CountIs(11);
    }
}
