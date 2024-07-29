namespace SouleRoyale;

internal sealed class Team
{
    public List<Player> Players { get; private set; }

    public Team() {
        Players = new(11);
        for (int i = 0; i < 11; i++) {
            Players.Add(new Player());
        }
    }
}
