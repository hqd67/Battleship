using System.Collections.Generic;

namespace BattleshipGame
{
    public class Player
    {
        public const int BoardSize = 10;

        public Cell[,] Board { get; private set; }
        public List<Ship> Ships { get; private set; }

        public Player()
        {
            Board = new Cell[BoardSize, BoardSize];
            Ships = new List<Ship>();

            for (int x = 0; x < BoardSize; x++)
                for (int y = 0; y < BoardSize; y++)
                    Board[x, y] = new Cell(x, y);
        }
    }
}
