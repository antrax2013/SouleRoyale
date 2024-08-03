using SouleRoyale.Move;

namespace SouleRoyale;

internal sealed class Player(IMovePlayer move, int lifePoint = 4, int number = 1, TeamsKey team = TeamsKey.Team1)
{
    private readonly IMovePlayer _move = move;
    private int _position = 0;

    public readonly int Number = number;
    public readonly TeamsKey Team = team;
    public int LifePoints { get; internal set; } = lifePoint;

    public int Position
    {
        get { return _position; }
        internal set
        {
            _position = value;
        }
    }
    public bool IsKo => LifePoints == 0;

    public void MoveFoward()
    {
        if (IsKo)
            throw new InvalidOperationException("A KO player cannot move.");
        _move.MoveFoward(this);
    }
    public void MoveBack()
    {
        if (IsKo)
            throw new InvalidOperationException("A KO player cannot move.");
        _move.MoveBack(this);
    }
}
