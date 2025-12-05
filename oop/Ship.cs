using System.Collections.Generic;

namespace BattleshipGame
{
    public class Ship
    {
        public List<Cell> Cells { get; } = new List<Cell>();

        public bool IsSunk =>
            Cells.TrueForAll(c => c.State == CellState.Hit);
    }
}
