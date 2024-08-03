using Gherkin.Ast;
using NFluent;
using NSubstitute;
using SouleRoyale.Move;
using Xunit.Gherkin.Quick;
using Feature = Xunit.Gherkin.Quick.Feature;

namespace SouleRoyale.Tests.TestCases;

[FeatureFile(@"./Features/Player.feature")]
public sealed class PlayerTests : Feature
{
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
    private Player _player;
    private readonly IMovePlayer _movePlayer = Substitute.For<IMovePlayer>();
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

    [Given("A player")]
    public void I_create_a_new_player()
    {
        _player = new(_movePlayer);
    }

    [Given("A player with 1 life point")]
    [When("I create a new player with 1 life point")]

    public void A_player_with_1_point_of_life()
    {
        _player = new(_movePlayer, 1);
    }

    [Given("A KO player")]
    public void A_KO_player()
    {
        _player = new(_movePlayer)
        {
            LifePoints = 0
        };
    }

    [And("The player position is (-?[0-3]{1})")]
    public void The_player_position_is(int position)
    {
        Check.That(_player.Position).IsEqualTo(position);
    }

    [Then("A InvalidOperationException fires with message A player cannot leave the game area.")]
    [Then("A InvalidOperationException fires with message A KO player cannot move.")]
    public static void Blank() { }

    #region Create player
    [Then("The player created have 1 life point")]
    public void The_player_created_have_1_life_points()
    {
        Check.That(_player.LifePoints).IsEqualTo(1);
    }   

    [And("The player is not KO")]
    public void The_player_is_not_KO()
    {
        Check.That(_player.IsKo).IsFalse();
    }
    #endregion Create player

    #region A KO player cannot move
    [When(@"The KO player move back")]
    public void The_KO_player_move_back()
    {
        Check.ThatCode(() => _player.MoveBack())
            .Throws<InvalidOperationException>()
            .WithMessage($"A KO player cannot move.");
    }

    [When(@"The KO player move foward")]
    public void The_KO_player_move_foward()
    {
        Check.ThatCode(() => _player.MoveFoward())
            .Throws<InvalidOperationException>()
            .WithMessage($"A KO player cannot move.");
    }
    #endregion A KO player cannot move

    #region A player becomes KO
    

    [When("On losing the last point of live")]
    public void On_losing_the_last_point_of_live()
    {
        _player.LifePoints--;
    }

    [Then("The player becomes KO")]
    public void The_player_becomes_KO()
    {
        Check.That(_player.IsKo).IsTrue();
    }
    #endregion A player becomes KO
}
