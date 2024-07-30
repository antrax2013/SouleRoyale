namespace SouleRoyale;

internal sealed class Player
{
    public int Number { get; internal set; }
    public string Team { get; internal set; }
    public int LifePoints { get; internal set; } = 4;

    public int Position { get; internal set; } = 0;
    public bool IsKo => LifePoints == 0;
}
