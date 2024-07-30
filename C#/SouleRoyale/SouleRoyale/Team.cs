namespace SouleRoyale;

internal sealed class Team
{
    public List<Player> Players { get; private set; }

    public Team(string team) {
        Players = new(11);
        for (int i = 0; i < 11; i++) {
            var player = new Player
            {
                Number = i + 1,
                Team = team
            };
            Players.Add(player);
        }
    }
}
