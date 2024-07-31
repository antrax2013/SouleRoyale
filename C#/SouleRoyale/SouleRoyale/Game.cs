
namespace SouleRoyale;

public enum TeamsKey
{
    Team1,
    Team2
}

public sealed class Game
{
    public readonly int MaxNumberOfTurn = 7;
    private readonly int AreaGameNumberOfLine = 3;
    public int NumberOfTurn { get; private set; } = 0;


    internal Dictionary<TeamsKey, Team> Teams { get; private set; } = [];
    internal Soule Soule;

    internal TeamsKey? Winner { get; private set; } = null;

    internal bool IsOver => Winner != null || NumberOfTurn == MaxNumberOfTurn;
    private bool IsGoal => Math.Abs(Soule.Position) == AreaGameNumberOfLine;


    internal TeamsKey ActiveTeam
    {
        get
        {
            return (NumberOfTurn % 2 == 0) ? TeamsKey.Team2 : TeamsKey.Team1;
        }
    }

    internal TeamsKey PassiveTeam
    {
        get
        {
            return (NumberOfTurn % 2 == 0) ? TeamsKey.Team1 : TeamsKey.Team2;
        }
    }

    public Game()
    {
        Teams.Add(TeamsKey.Team1, new Team(TeamsKey.Team1, AreaGameNumberOfLine));
        Teams.Add(TeamsKey.Team2, new Team(TeamsKey.Team2, AreaGameNumberOfLine));
        Soule = new Soule();
    }

    public void InitializePositions(string instructions, TeamsKey teamKey)
    {
        var positions = instructions.Split(' ').Select(i => Convert.ToInt32(i)).ToArray();
        var players = Teams[teamKey].Players;

        for (int i = 0; i < positions.Length; i++)
        {
            var value = positions[i];

            if (value < 1 || value > 3)
                throw new InvalidOperationException($"The initial player position must beetween 1 and 3, current value: {value} is wrong.");
            
            var player = players[i];
            player.Position = IsFirstTeamPlayer(player) ? -value : value;
        }
    }

    private static bool IsFirstTeamPlayer(Player player) => player.Team == TeamsKey.Team1;

    private Player GetPlayer(int playerIndex, TeamsKey targetedTeam)
    {
        return Teams[targetedTeam].Players[playerIndex];
    }

    private void ApplyInstruction(string instruction, Player player)
    {
        if (player.IsKo || IsOver)
            return;

        switch (instruction)
        {
            case "+": 
                MoveFoward(player);
                if (IsGoal) {
                    Winner = player.Team;
                }
                break;
            case "-": MoveBack(player); break;
            case "0": break;
            case "X": break;
            default:
                var targetedPlayerIndex = Convert.ToInt32(instruction) - 1;
                var targetedTeam = player.Team == TeamsKey.Team1 ? TeamsKey.Team2 : TeamsKey.Team1;
                Hit(player, GetPlayer(targetedPlayerIndex, targetedTeam)); 
                break;
        }
    }

    private static void Hit(Player player, Player targetedPlayer)
    {
        if (player.Position == targetedPlayer.Position && !targetedPlayer.IsKo)
        {
            targetedPlayer.LifePoints--;
        }
    }

    private static void MoveBack(Player player)
    {
        if (IsFirstTeamPlayer(player))
            player.Position--;
        else player.Position++;
    }

    private bool MoveFoward(Player player)
    {
        var delta = IsFirstTeamPlayer(player) ? 1 : -1;
        player.Position += delta;
        return MoveSouleIfNeeded(player.Position, delta);
    }

    private bool MoveSouleIfNeeded(int playerPosition, int delta)
    {
        if (Soule.Position == playerPosition)
        {
            Soule.Position += delta;
        }
        return IsGoal;
    }

    internal void ReadInsturctions(string item1, string item2)
    {
        if(IsOver)
        {
            return;
        }            

        NumberOfTurn++;
        var team1IsActiveTeam = ActiveTeam == TeamsKey.Team1;
        var activeTeamInstruction = team1IsActiveTeam ? item1.Split(" ") : item2.Split(" ");
        var passiveTeamInstruction = team1IsActiveTeam ? item2.Split(" ") : item1.Split(" ");
        int playerIndex = 0;

        while (playerIndex < 11 && !IsOver)
        {
            ApplyInstruction(activeTeamInstruction[playerIndex], GetPlayer(playerIndex, ActiveTeam));
            ApplyInstruction(passiveTeamInstruction[playerIndex], GetPlayer(playerIndex, PassiveTeam));
            playerIndex++;
        }
    }
}
