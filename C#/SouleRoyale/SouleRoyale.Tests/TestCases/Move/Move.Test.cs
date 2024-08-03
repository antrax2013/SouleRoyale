using Gherkin.Ast;
using NFluent;
using SouleRoyale.Move;
using Xunit.Gherkin.Quick;
using Feature = Xunit.Gherkin.Quick.Feature;

namespace SouleRoyale.Tests.TestCases.Move;

[FeatureFile(@"./Features/Move/Move.feature")]
public sealed class MoveTest : Feature
{
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
    private Player _player;
    private readonly MovePlayer _movePlayer = new();
    private Exception? _exception;
    private int? _positionSaved;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.


    [Then(@"His position should not change")]
    public void His_position_should_not_change()
    {
        Check.That(_player.Position).IsEqualTo(_positionSaved);
        _positionSaved = null;
    }

    [And(@"A invalid operation exception fires with message: 'A player cannot leave the game area.'")]
    public void A_invalid_operation_exception_fires_with_message()
    {
        Check.That(_exception)
            .IsNotNull()
            .And.IsInstanceOf<InvalidOperationException>()
            .And.WhichMember(ex => ex!.Message).Equals("A player cannot leave the game area.");
        _exception = null;
    }

    #region Move a player
    [Given(@"I create a new player in position 0")]
    public void I_create_a_new_player_in_position_0()
    {
        _player = new Player(_movePlayer);
    }

    [When("I move the player, his position change")]
    public void I_move_the_player_his_position_change(DataTable dataTable)
    {
        foreach (TableRow? row in dataTable.Rows.Skip(1))
        {
            // Given
            _player.Position = 0;
            string moveTypeName = row.Cells.ElementAt(0).Value;
            int expectedPosition = Convert.ToInt32(row.Cells.ElementAt(1).Value);

            // When
            if (moveTypeName == "MoveForward")
                _player.MoveFoward();
            else
                _player.MoveBack();

            // Then
            Check.That(_player.Position).IsEqualTo(expectedPosition);
        }
    }
    #endregion Move a player

    #region  Not should move back a player out of the area
    [Given(@"I create a new player in his goal line position")]
    public void I_create_a_new_player_his_goal_line_position()
    {
        _positionSaved = -3;
        _player = new Player(_movePlayer) { Position = -3 };
    }

    [When(@"I move back the player")]
    public void I_moveback_the_player()
    {
        try
        {
            _player.MoveBack();
        }
        catch (InvalidOperationException ex)
        {
            _exception = ex;
        }
    }
    #endregion  Not should move back a player out of the area

    #region  Not should move foward a player out of the area
    [Given(@"I create a new player in the overside goal line position")]
    public void I_create_a_new_player_in_the_overside_goal_line_position()
    {
        _positionSaved = 3;
        _player = new Player(_movePlayer) { Position = 3 };
    }

    [When(@"I move forward the player")]
    public void I_forward_the_player()
    {
        try
        {
            _player.MoveFoward();
        }
        catch (InvalidOperationException ex)
        {
            _exception = ex;
        }
    }
    #endregion  Not should move foward a player out of the area
}