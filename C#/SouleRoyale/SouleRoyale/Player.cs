namespace SouleRoyale;

internal sealed class Player(int number=1, TeamsKey team=TeamsKey.Team1, int maxPosition = 3)
{
    public readonly int Number = number;
    public readonly TeamsKey Team = team;
    public readonly int MaxPosition = maxPosition;
    public int LifePoints { get; internal set; } = 4;

    private int _position = 0;
    public int Position
    {
        get { return _position; }
        internal set
        {
            if(IsKo)
                throw new InvalidOperationException("A KO player cannot move.");
            if (Math.Abs(value) > MaxPosition)
                throw new InvalidOperationException("A player cannot leave the game area.");
            _position = value;
        }
    }
    public bool IsKo => LifePoints == 0;
}
