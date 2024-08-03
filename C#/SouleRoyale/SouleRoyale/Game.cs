using Newtonsoft.Json;
using SouleRoyale.Move;
using System.Diagnostics;

namespace SouleRoyale;


public enum TeamsKey
{
    Team1,
    Team2
}

public sealed class Game
{
    public readonly int MaxNumberOfTurn = 7;
    private readonly int _areaGameNumberOfLine = 3;
    private readonly int _numberOfPlayers = 11;
    private readonly int _numberOfLifePointsByPlayer = 4;

    public int NumberOfTurn { get; private set; } = 0;

    internal Dictionary<TeamsKey, List<Player>> Teams { get; private set; } = [];
    internal Soule Soule;

    internal TeamsKey? Winner { get; private set; } = null;

    internal bool IsOver => Winner != null || NumberOfTurn == MaxNumberOfTurn;
    private bool IsGoal => Math.Abs(Soule.Position) == _areaGameNumberOfLine;

    internal TeamsKey ActiveTeam
    {
        get => (NumberOfTurn % 2 == 0) ? TeamsKey.Team2 : TeamsKey.Team1;
    }

    internal TeamsKey PassiveTeam
    {
        get => (NumberOfTurn % 2 == 0) ? TeamsKey.Team1 : TeamsKey.Team2;
    }

    public Game()
    {
        Teams.Add(TeamsKey.Team1, BuildTeam(TeamsKey.Team1, new MovePlayer(_areaGameNumberOfLine)));
        Teams.Add(TeamsKey.Team2, BuildTeam(TeamsKey.Team2, new MirrorMovePlayer(_areaGameNumberOfLine)));
        Soule = new Soule();
    }

    private List<Player> BuildTeam(TeamsKey teamsKey, IMovePlayer move)
    {
        List<Player> Players = new(_numberOfPlayers);
        for (int i = 0; i < _numberOfPlayers; i++)
            Players.Add(new Player(move, _numberOfLifePointsByPlayer, i + 1, teamsKey));

        return Players;
    }

    public void InitializePositions(string instructions, TeamsKey teamKey)
    {
        int[] positions = instructions.Split(' ').Select(i => Convert.ToInt32(i)).ToArray();
        List<Player> players = Teams[teamKey];

        for (int i = 0; i < positions.Length; i++)
        {
            int value = positions[i];

            if (value < 1 || value > 3)
                throw new InvalidOperationException($"The initial player position must beetween 1 and 3, current value: {value} is wrong.");

            Player player = players[i];
            player.Position = IsFirstTeamPlayer(player) ? -value : value;
        }
    }

    private static bool IsFirstTeamPlayer(Player player) => player.Team == TeamsKey.Team1;

    private Player GetPlayer(int playerIndex, TeamsKey targetedTeam)
    {
        return Teams[targetedTeam][playerIndex];
    }

    private void ApplyInstruction(string instruction, Player player)
    {
        if (player.IsKo || IsOver)
            return;

        switch (instruction)
        {
            case "+":
                MoveFoward(player);
                if (IsGoal)
                    Winner = player.Team;
                break;
            case "-": player.MoveBack(); break;
            case "0":
            case "x":
            case "X": break;
            default:
                int targetedPlayerIndex = Convert.ToInt32(instruction) - 1;
                TeamsKey targetedTeam = player.Team == TeamsKey.Team1 ? TeamsKey.Team2 : TeamsKey.Team1;
                Hit(player, GetPlayer(targetedPlayerIndex, targetedTeam));
                break;
        }
    }

    private static void Hit(Player player, Player targetedPlayer)
    {
        if (player.Position == targetedPlayer.Position && !targetedPlayer.IsKo)
            targetedPlayer.LifePoints--;
    }

    private void MoveFoward(Player player)
    {
        player.MoveFoward();
        MoveSouleIfNeeded(player.Position, IsFirstTeamPlayer(player) ? 1 : -1);
    }

    private void MoveSouleIfNeeded(int playerPosition, int delta)
    {
        if (Soule.Position == playerPosition)
            Soule.Position += delta;
    }

    internal void ReadInsturctions(string item1, string item2)
    {
        if (IsOver)
            return;

        NumberOfTurn++;
        bool team1IsActiveTeam = ActiveTeam == TeamsKey.Team1;
        string[] activeTeamInstruction = team1IsActiveTeam ? item1.Split(" ") : item2.Split(" ");
        string[] passiveTeamInstruction = team1IsActiveTeam ? item2.Split(" ") : item1.Split(" ");
        int playerIndex = 0;

        while (playerIndex < _numberOfPlayers && !IsOver)
        {
            ApplyInstruction(activeTeamInstruction[playerIndex], GetPlayer(playerIndex, ActiveTeam));
            ApplyInstruction(passiveTeamInstruction[playerIndex], GetPlayer(playerIndex, PassiveTeam));
            playerIndex++;
        }

        Debug.WriteLine($"Round: {NumberOfTurn} Soule: {Soule.Position}");
        Debug.WriteLine(JsonConvert.SerializeObject(Teams[TeamsKey.Team1].Select(p => new { p.Number, p.Position, p.LifePoints })));
        Debug.WriteLine(JsonConvert.SerializeObject(Teams[TeamsKey.Team2].Select(p => new { p.Number, p.Position, p.LifePoints })));

        if (Winner != null)
            Debug.WriteLine($"The winner is: {Winner} !!!");

        else if (IsOver)
            Debug.WriteLine($"Equality, the game is over.");
    }
}
