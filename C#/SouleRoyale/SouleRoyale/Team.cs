namespace SouleRoyale;

internal sealed class Team
{
    public static readonly int NUMBER_OF_PLAYERS = 11;
    public List<Player> Players { get; private set; }

    public Team(TeamsKey teamsKey=TeamsKey.Team1, int areaGameNumberOfLine=3) {
        Players = new(NUMBER_OF_PLAYERS);
        for (int i = 0; i < NUMBER_OF_PLAYERS; i++) {
            Players.Add(new Player(i + 1, teamsKey, areaGameNumberOfLine));
        }
    }
}
