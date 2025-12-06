using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BattleshipGame
{
    public partial class Form1 : Form
    {
        private Button[,] playerButtons = new Button[10, 10];
        private Button[,] enemyButtons = new Button[10, 10];

        private TextBox ipBox;
        private TextBox portBox;
        private Button hostBtn;
        private Button joinBtn;
        private Button botBtn;
        private Button autoBtn;
        private Label sizeLabel;
        private NumericUpDown sizeBox;
        private Label statusLabel;

        public Form1()
        {
            InitializeComponent();
            BuildUiRuntime();
        }

        private void BuildUiRuntime()
        {
            this.Text = "Battleship - Network";
            this.ClientSize = new Size(750, 520);

            int size = 30;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    var btn = new Button
                    {
                        Location = new Point(10 + x * size, 10 + y * size),
                        Size = new Size(size, size),
                        BackColor = Color.LightBlue,
                        Tag = new Point(x, y)
                    };
                    playerButtons[x, y] = btn;
                    Controls.Add(btn);

                    var ebtn = new Button
                    {
                        Location = new Point(350 + x * size, 10 + y * size),
                        Size = new Size(size, size),
                        BackColor = Color.LightBlue,
                        Tag = new Point(x, y)
                    };
                    enemyButtons[x, y] = ebtn;
                    Controls.Add(ebtn);
                }
            }

            ipBox = new TextBox { Location = new Point(10, 330), Width = 120, Text = "127.0.0.1" };
            Controls.Add(ipBox);
            portBox = new TextBox { Location = new Point(140, 330), Width = 70, Text = "5000" };
            Controls.Add(portBox);

            hostBtn = new Button { Location = new Point(220, 325), Size = new Size(100, 30), Text = "Создать игру" };
            Controls.Add(hostBtn);

            joinBtn = new Button { Location = new Point(330, 325), Size = new Size(100, 30), Text = "Подключиться" };
            Controls.Add(joinBtn);

            botBtn = new Button { Location = new Point(440, 325), Size = new Size(120, 30), Text = "Игра с ботом" };
            Controls.Add(botBtn);

            autoBtn = new Button { Location = new Point(570, 325), Size = new Size(120, 30), Text = "Авто-расстановка" };
            Controls.Add(autoBtn);

            sizeLabel = new Label { Location = new Point(10, 370), Text = "Размер: 4", AutoSize = true };
            Controls.Add(sizeLabel);
            sizeBox = new NumericUpDown { Location = new Point(80, 370), Minimum = 1, Maximum = 4, Value = 4 };
            Controls.Add(sizeBox);

            statusLabel = new Label { Location = new Point(10, 400), Text = "Статус: Готов", AutoSize = true };
            Controls.Add(statusLabel);
        }
        private GameManager game = new GameManager();
        private int selectedShipSize = 4;
        private Orientation currentOrientation = Orientation.Horizontal;
        private Dictionary<int, int> shipLimits = new Dictionary<int, int> { { 4, 1 }, { 3, 2 }, { 2, 3 }, { 1, 4 } };
        private Dictionary<int, int> shipsPlaced = new Dictionary<int, int> { { 4, 0 }, { 3, 0 }, { 2, 0 }, { 1, 0 } };

        private void ResetShipsPlaced()
        {
            foreach (var key in shipLimits.Keys)
                shipsPlaced[key] = 0;
        }

        private void PlayerGrid_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Point pos = (Point)btn.Tag;
            int x = pos.X;
            int y = pos.Y;

            if (e.Button == MouseButtons.Right)
            {
                currentOrientation = currentOrientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;
                return;
            }

            if (shipsPlaced[selectedShipSize] >= shipLimits[selectedShipSize])
            {
                MessageBox.Show($"Все корабли размера {selectedShipSize} уже размещены");
                return;
            }

            if (CanPlaceShipPlayer(x, y, selectedShipSize, currentOrientation))
            {
                PlaceShipPlayer(x, y, selectedShipSize, currentOrientation);
                shipsPlaced[selectedShipSize]++;
                RefreshField();
            }
        }

        private bool CanPlaceShipPlayer(int x, int y, int size, Orientation orientation)
        {
            for (int i = 0; i < size; i++)
            {
                int cx = x + (orientation == Orientation.Horizontal ? i : 0);
                int cy = y + (orientation == Orientation.Vertical ? i : 0);
                if (cx >= 10 || cy >= 10) return false;
                if (game.LocalPlayer.Grid[cx, cy].State != CellState.Empty) return false;
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = cx + dx, ny = cy + dy;
                        if (nx >= 0 && ny >= 0 && nx < 10 && ny < 10)
                            if (game.LocalPlayer.Grid[nx, ny].State == CellState.Ship)
                                return false;
                    }
            }
            return true;
        }

        private void PlaceShipPlayer(int x, int y, int size, Orientation orientation)
        {
            Ship ship = new Ship { Orientation = orientation };
            for (int i = 0; i < size; i++)
            {
                int cx = x + (orientation == Orientation.Horizontal ? i : 0);
                int cy = y + (orientation == Orientation.Vertical ? i : 0);
                var cell = game.LocalPlayer.Grid[cx, cy];
                cell.State = CellState.Ship;
                cell.Ship = ship;
                ship.Cells.Add(cell);
            }
            game.LocalPlayer.Ships.Add(ship);
        }

    }
}
