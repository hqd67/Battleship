using System;

namespace BattleshipGame
{
    public class GameManager
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public Player CurrentPlayer { get; private set; }
        public Player OpponentPlayer => CurrentPlayer == Player1 ? Player2 : Player1;

        public GameManager()
        {
            Player1 = new Player();
            Player2 = new Player();
            CurrentPlayer = Player1;
        }

        public bool Shoot(int x, int y)
        {
            var cell = OpponentPlayer.Board[x, y];

            if (cell.State == CellState.Hit || cell.State == CellState.Miss)
                return false;

            if (cell.State == CellState.Ship)
            {
                cell.State = CellState.Hit;
                return true;
            }
            else
            {
                cell.State = CellState.Miss;
                EndTurn();
                return false;
            }
        }

        public void EndTurn()
        {
            CurrentPlayer = OpponentPlayer;
        }

        public bool CheckVictory(Player target)
        {
            foreach (var ship in target.Ships)
                if (!ship.IsSunk) return false;

            return true;
        }
    }
}
