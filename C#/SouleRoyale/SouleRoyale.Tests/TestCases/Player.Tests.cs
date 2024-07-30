using Xunit.Gherkin.Quick;
using NFluent;
using System.Numerics;

namespace SouleRoyale.Tests.TestCases;

//Gherkin.Quick
//https://www.tutisani.com/bdd/
//https://youtu.be/RBcJYt2g_gE
//https://github.com/ttutisani/Xunit.Gherkin.Quick

//Cocumber.Net
//https://github.com/cucumber/gherkin/blob/main/dotnet/README.md

[FeatureFile(@"./Features/Player.feature")]
public sealed class PlayerTests : Feature
{
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
    private Player _player;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

    [Given(@"I ask to create a new player")]
    public void I_ask_to_create_a_new_player() { }

    [When(@"I create a new player")]
    public void I_create_a_new_player()
    {
        _player = new Player();
    }

    [Then(@"The player created have 4 life points")]
    public void The_player_created_have_4_life_points()
    {
        Check.That(_player.LifePoints).IsEqualTo(4);
    }

    [And(@"The player position is 0")]
    public void The_player_position_is_0()
    {
        Check.That(_player.Position).IsEqualTo(0);
    }

    [And(@"The player is not ko")]
    public void The_player_is_not_ko()
    {
        Check.That(_player.IsKo).IsFalse();
    }

}
