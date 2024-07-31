using Xunit.Gherkin.Quick;
using NFluent;
using System.Numerics;
using Gherkin.Ast;
using Feature = Xunit.Gherkin.Quick.Feature;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections.Generic;

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

    [Given("A player")]
    [When("I create a new player")]
    public void I_create_a_new_player()
    {
        _player = new();
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
    [Then("The player created have 4 life points")]
    public void The_player_created_have_4_life_points()
    {
        Check.That(_player.LifePoints).IsEqualTo(4);
    }   

    [And("The player is not KO")]
    public void The_player_is_not_KO()
    {
        Check.That(_player.IsKo).IsFalse();
    }
    #endregion Create player

    #region Wrong Move
    [When(@"After a move, the player receives a wrong position:")]
    public void After_a_move_the_player_receives_a_wrong_position(DataTable dataTable)
    {
        foreach (var row in dataTable.Rows.Skip(1))
        {
            // Given
            var wrongPosition = Convert.ToInt32(row.Cells.ElementAt(0).Value.Split(" ").First());

            // When/Then
            Check.ThatCode(() => _player.Position = wrongPosition)
            .Throws<InvalidOperationException>()
            .WithMessage($"A player cannot leave the game area.");

        }
    }
    #endregion Wrong Move

    #region A KO player cannot move
    [Given("A KO player")]
    public void A_KO_player()
    {
        _player = new()
        {
            LifePoints = 0
        };
    }

    [When(@"After a move, the player receives a new position")]
    public void After_a_move_a_player_receives_a_wrong_position()
    {
        var newPosition = 2;
        Check.ThatCode(() => _player.Position = newPosition)
            .Throws<InvalidOperationException>()
            .WithMessage($"A KO player cannot move.");
    }
    #endregion A KO player cannot move

    #region A player becomes KO
    [Given("A player with 1 point of life")]
    public void A_player_with_1_point_of_life()
    {
        _player = new()
        {
            LifePoints = 1
        };
    }

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
