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
    }
}
