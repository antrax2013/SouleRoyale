using Gherkin.Ast;
using Newtonsoft.Json.Linq;
using NFluent;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using Xunit.Gherkin.Quick;
using Feature = Xunit.Gherkin.Quick.Feature;

namespace SouleRoyale.Tests.TestCases;

[FeatureFile(@"./Features/Game.feature")]
public class GameTests : Feature
{
    private Game _game;

    [When(@"I create a new game")]
    [Given(@"a new game")]
    public void I_create_a_new_game()
    {
        _game = new Game();
    }

    #region Create a game

    [Given(@"I ask to create a new game")]
    public void I_ask_to_create_a_new_game() { }    

    [Then(@"2 teams have been created")]
    public void _2_teams_have_been_created() {
        Check.That(_game.Teams).CountIs(2);
    }

    [And(@"Teams have 11 players")]
    public void Teams_have_11_players() {
        var teamsWith11Playes = _game.Teams.Where(t => t.Players.Count==11).ToList();
        Check.That(_game.Teams).CountIs(2);
    }

    [And(@"The first team is active")]
    public void The_first_team_is_active() { 
        var firstTeam = _game.Teams.First();
        var activeTeam = _game.ActiveTeam;

        Check.That(activeTeam).IsEqualTo(firstTeam);
    }

    [And(@"The turn number is 0")]
    public void The_turn_number_is_0() {
        var numberOfTurn = _game.NumberOfTurn;

        Check.That(numberOfTurn).IsEqualTo(0);
    }

    [And(@"The max turn number is 7")]
    public void The_max_turn_number_is_7() {
        Check.That(_game.MaxNumberOfTurn).IsEqualTo(7);
    }
    #endregion Create a game

    #region Set players of first team intial positions
    [When(@"captain of first team gives the following instructions ([0-9 ]+)")]
    public void Captain_of_first_team_give_the_following_instructions(string instructions)
    {
        _game.InitializePositions(instructions);
    }

    [Then(@"playes are in 2 1 1 1 1 2 1 1 1 1 3 lines")]
    public void Players_are()
    {
        int[] expectedPosition = [2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 3];
        var firstTeamsPlayer = _game.Teams.First().Players;

        for (int i = 0; i < 11; i++)
        {
            Check.That(firstTeamsPlayer[i].Position).IsEqualTo(expectedPosition[i]);
        }
    }
    #endregion Set players of first team intial positions

    #region Set players of first team invalide intial positions
    [Given(@"captain of first team gives the following instructions a invalid operation exception fires:")]
    public void Captain_of_first_team_give_the_following_instructions(DataTable dataTable)
    {
        foreach (var row in dataTable.Rows.Skip(1))
        {
            // Given
            var game = new Game();
            var instructions = string.Join(" ",row.Cells.ElementAt(0).Value);
            var wrongValue = row.Cells.ElementAt(0).Value.Split(" ").First();

            // When/Then
            Check.ThatCode(() => game.InitializePositions(instructions))
            .Throws<InvalidOperationException>()
            .WithMessage($"The initial player position must beetween 1 and 3, current value: { wrongValue } is wrong.");

        }
    }
    #endregion Set players of first team invalide intial positions
}
