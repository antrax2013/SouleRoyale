
namespace SouleRoyale;

public sealed class Game
{
    const int MAX_NUMBER_OF_TURN = 7;

    public static int MaxNumberOfTurn => MAX_NUMBER_OF_TURN;

    internal Team? Winner { get; private set; } = null;

    internal bool IsOver => Winner != null;

    internal List<Team> Teams { get; private set; }

    internal Soule Soule { get; private set; }

    public int NumberOfTurn { get; private set; } = 0;
    internal Team ActiveTeam
    {
        get
        {
            if (NumberOfTurn == 0 || (NumberOfTurn % 2 == 1))
                return Teams.First();
            return Teams.Last();
        }
    }

    private bool IsGoal => Math.Abs(Soule.Position) == 3;

    public Game()
    {
        Teams = [new Team("Equipe 1"), new Team("Equipe 2")];
        Soule = new Soule();
    }

    public void InitializePositions(string instructions, bool firstTeam)
    {
        var positions = instructions.Split(' ').Select(i => Convert.ToInt32(i)).ToArray();
        var players = firstTeam ? Teams.First().Players : Teams.Last().Players;

        for (int i = 0; i < positions.Length; i++)
        {
            var value = positions[i];

            if (value < 1 || value > 3)
                throw new InvalidOperationException($"The initial player position must beetween 1 and 3, current value: {value} is wrong.");
            
            var player = players[i];
            player.Position = IsFirstTeamPlayer(player) ? -value : value;
        }
    }

    private bool IsFirstTeamPlayer(Player player) => Teams.First().Players.Contains(player);

    private Player GetPlayer(int numberOfPlayer, bool activeTeam)
    {
        int playerIndex = numberOfPlayer - 1;
        if (activeTeam)
        {
            return ActiveTeam.Players[playerIndex];
        }
        else
        {
            return (Teams.First(t => t != ActiveTeam)).Players[playerIndex];
        }
    }

    private void ApplyInstruction(string instruction, Player player, bool activeTeam)
    {
        if (player.IsKo)
            return;

        switch (instruction)
        {
            case "+": MoveFoward(player); break;
            case "-": MoveBack(player); break;
            case "0": break;
            case "X": break;
            default: Hit(player, GetPlayer(Convert.ToInt32(instruction), !activeTeam)); break;
        }
    }

    private void Hit(Player player, Player targetedPlayer)
    {
        if (player.Position == targetedPlayer.Position && !targetedPlayer.IsKo)
        {
            targetedPlayer.LifePoints--;
        }
    }

    private void MoveBack(Player player)
    {
        if (IsFirstTeamPlayer(player))
            player.Position--;
        else player.Position++;
    }

    private void MoveFoward(Player player)
    {
        var delta = IsFirstTeamPlayer(player) ? 1 : -1;
        player.Position += delta;
        MoveSouleIfNeeded(player.Position, delta);
    }

    private void MoveSouleIfNeeded(int playerPosition, int delta)
    {
        if (Soule.Position == playerPosition)
        {
            Soule.Position += delta;
        }
    }

    internal void ReadInsturctions(string item1, string item2)
    {
        NumberOfTurn++;
        var team1IsActiveTeam = ActiveTeam == Teams.First();
        var activeTeamInstruction = team1IsActiveTeam ? item1.Split(" ") : item2.Split(" ");
        var passiveTeamInstruction = team1IsActiveTeam ? item2.Split(" ") : item1.Split(" ");
        int playerNumber = 1;

        while (playerNumber <= 11)
        {
            var index = playerNumber - 1;
            ApplyInstruction(activeTeamInstruction[index], GetPlayer(playerNumber, true), true);
            if (IsGoal)
            {
                Winner = ActiveTeam;
                return;
            }

            ApplyInstruction(passiveTeamInstruction[index], GetPlayer(playerNumber, false), false);
            if (IsGoal)
            {
                Winner = Teams.First(t => t != ActiveTeam);
                return;
            }
            playerNumber++;
        }
    }
}
