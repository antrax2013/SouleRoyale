
namespace SouleRoyale;

public sealed class Game
{
    const int MAX_NUMBER_OF_TURN = 7;

    public int MaxNumberOfTurn => MAX_NUMBER_OF_TURN;

    internal List<Team> Teams { get; private set; }

    public int NumberOfTurn { get; private set; } = 0;
    internal Team ActiveTeam { 
        get { 
           if(NumberOfTurn == 0 || (NumberOfTurn % 2 == 1))
                return Teams.First();
           return Teams.Last();
        } }

    public Game() {
        Teams = [new Team(), new Team()];
    }

    public void InitializePositions(string instructions)
    {
        var positions = instructions.Split(' ').Select(i => Convert.ToInt32(i)).ToArray();
        var players = ActiveTeam.Players;

        for (int i = 0; i < positions.Length; i++) {
            var value = positions[i];

            if(value < 1 || value > 3)
                throw new InvalidOperationException($"The initial player position must beetween 1 and 3, current value: {value} is wrong.");

            players[i].Position = positions[i];
        }
    }
}
