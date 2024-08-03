namespace SouleRoyale.Move
{
    internal sealed class MovePlayer(int absoluteMaxPosition=3) : IMovePlayer
    {
        public void MoveBack(Player player)
        {
            if (player.IsKo)
                throw new InvalidOperationException("A KO player cannot move.");
            if(player.Position == -absoluteMaxPosition)
                throw new InvalidOperationException("A player cannot leave the game area.");
            player.Position--;
        }

        public void MoveFoward(Player player)
        {
            if (player.IsKo)
                throw new InvalidOperationException("A KO player cannot move.");
            if (player.Position == absoluteMaxPosition)
                throw new InvalidOperationException("A player cannot leave the game area.");
            player.Position++;
        }
    }
}
