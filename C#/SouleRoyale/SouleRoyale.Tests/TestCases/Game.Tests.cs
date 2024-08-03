using Gherkin.Ast;
using NFluent;
using Xunit.Gherkin.Quick;
using Feature = Xunit.Gherkin.Quick.Feature;

namespace SouleRoyale.Tests.TestCases;

[FeatureFile(@"./Features/Game.feature")]
public class GameTests : Feature
{
    private Game _game = new();

    [When(@"I create a new game")]
    [Given(@"a new game")]
    public void I_create_a_new_game()
    {
        _game = new Game();
    }

    [When(@"captains give the following instructions:")]
    public void Captains_give_the_following_instructions(DataTable dataTable)
    {
        // Given
        _game = new Game();

        Queue<Tuple<string, string>> instructions = new Queue<Tuple<string, string>>(dataTable
            .Rows.Skip(1)
            .Select(r => new Tuple<string, string>(
                string.Join(" ", r.Cells.ElementAt(0).Value),
                string.Join(" ", r.Cells.ElementAt(1).Value)
                )
            ));
        Tuple<string, string> initiales = instructions.Dequeue();
        _game.InitializePositions(initiales.Item1, TeamsKey.Team1);
        _game.InitializePositions(initiales.Item2, TeamsKey.Team2);

        // When
        while (
            instructions.TryDequeue(out Tuple<string, string>? instruction)
            && !_game.IsOver
        )
        {
            Check.That(instruction).IsNotNull();
            _game.ReadInsturctions(instruction.Item1, instruction.Item2);
        }
    }

    [And(@"The game is over on round ([3-7]{1})")]
    [Then(@"The game is over on round ([3-7]{1})")]
    public void The_game_is_over_on_3rd_round(int roundNumber)
    {
        Check.That(_game.NumberOfTurn).IsEqualTo(roundNumber);
        Check.That(_game.IsOver).IsTrue();
    }

    [And(@"Winner is null")]
    [And(@"No winner")]
    public void The_winner_is_null()
    {
        Check.That(_game.Winner).IsNull();
    }

    #region Create a game

    [Given(@"I ask to create a new game")]
    public static void I_ask_to_create_a_new_game() { }

    [Then(@"2 teams have been created")]
    public void _2_teams_have_been_created()
    {
        Check.That(_game.Teams).CountIs(2);
    }

    [And(@"Teams have 11 players")]
    public void Teams_have_11_players()
    {
        List<KeyValuePair<TeamsKey, List<Player>>> teamsWith11Playes = _game.Teams.Where(t => t.Value.Count == 11).ToList();
        Check.That(_game.Teams).CountIs(2);
    }

    [And(@"The turn number is 0")]
    public void The_turn_number_is_0()
    {
        int numberOfTurn = _game.NumberOfTurn;

        Check.That(numberOfTurn).IsEqualTo(0);
    }

    [And(@"The soule is in 0 position")]
    public void The_soule_is_in_0_position()
    {
        Soule soule = _game.Soule;

        Check.That(soule.Position).IsEqualTo(0);
    }

    [And(@"The max turn number is 7")]
    public void The_max_turn_number_is_7()
    {
        Check.That(_game.MaxNumberOfTurn).IsEqualTo(7);
    }

    [And(@"The game is not over")]
    public void The_game_is_not_over()
    {
        Check.That(_game.IsOver).IsFalse();
    }
    #endregion Create a game

    #region Set players of first team intial positions
    [When(@"captain of first team gives the following instructions ([0-9 ]+)")]
    public void Captain_of_first_team_give_the_following_instructions(string instructions)
    {
        _game.InitializePositions(instructions, TeamsKey.Team1);
    }

    [Then(@"players are in -2 -1 -1 -1 -1 -2 -1 -1 -1 -1 -3 lines")]
    public void Players_are()
    {
        int[] expectedPosition = [-2, -1, -1, -1, -1, -2, -1, -1, -1, -1, -3];
        List<Player> firstTeamsPlayer = _game.Teams[TeamsKey.Team1];

        for (int i = 0; i < 11; i++)
        {
            Check.That(firstTeamsPlayer[i].Position).IsEqualTo(expectedPosition[i]);
        }
    }
    #endregion Set players of first team intial positions

    #region Set players of first team invalid intial positions
    [Given(@"captain of first team gives the following instructions a invalid operation exception fires:")]
    public static void Captain_of_first_team_give_the_following_instructions(DataTable dataTable)
    {
        foreach (TableRow? row in dataTable.Rows.Skip(1))
        {
            // Given
            Game game = new Game();
            string instructions = string.Join(" ", row.Cells.ElementAt(0).Value);
            string wrongValue = row.Cells.ElementAt(0).Value.Split(" ").First();

            // When/Then
            Check.ThatCode(() => game.InitializePositions(instructions, TeamsKey.Team1))
            .Throws<InvalidOperationException>()
            .WithMessage($"The initial player position must beetween 1 and 3, current value: {wrongValue} is wrong.");

        }
    }
    #endregion Set players of first team invalid intial positions

    #region A game winned by first team at thrid turn

    [And(@"The first Team is the winner")]
    public void The_first_Team_is_the_winner()
    {
        Check.That(_game.Winner).IsEqualTo(TeamsKey.Team1);
    }

    [And(@"The number ([1-9]|10|11) of first team is ko")]
    public void The_player_of_first_Team_is_ko(int playerNumber)
    {
        Player player = _game.Teams[TeamsKey.Team1].ElementAt(playerNumber - 1);
        Check.That(player.IsKo).IsTrue();
    }
    #endregion A game winned by first team at thrid turn

    #region A game winned by second team at round
    [And(@"The second Team is the winner")]
    public void The_second_Team_is_the_winner()
    {
        Check.That(_game.Winner).IsEqualTo(TeamsKey.Team2);
    }

    [And(@"The number ([1-9]|10|11) of second team is ko")]
    public void The_player_of_the_second_is_ko(int playerNumber)
    {
        Player player = _game.Teams[TeamsKey.Team2].ElementAt(playerNumber - 1);
        Check.That(player.IsKo).IsTrue();
    }

    [And(@"The number ([1-9]|10|11) of first team is in line (-?[0-3]{1})")]
    public void The_number_X_of_first_team_is_in_line(int playerNumber, int position)
    {
        Player player = _game.Teams[TeamsKey.Team1].ElementAt(playerNumber - 1);
        Check.That(player.Position).IsEqualTo(position);
    }
    #endregion A game winned by second team at round
}