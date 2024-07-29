namespace SouleRoyale;

internal sealed class Player
{
    public int LifePoints { get; private set; } = 4;
    public int Position { get; internal set; } = 0;
}
