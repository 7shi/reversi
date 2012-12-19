using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reversi
{
    public partial class Form1 : Form
    {
        int[,] board = new int[8, 8];

        public Form1()
        {
            InitializeComponent();
            Text = "リバーシ";
            ClientSize = new Size(260, 260);
            BackColor = Color.Green;
            board[3, 3] = 2;
            board[4, 3] = 1;
            board[3, 4] = 1;
            board[4, 4] = 2;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int i = 0; i <= 8; i++)
            {
                e.Graphics.DrawLine(Pens.Black, i * 30 + 10, 10, i * 30 + 10, 250);
                e.Graphics.DrawLine(Pens.Black, 10, i * 30 + 10, 250, i * 30 + 10);
            }
            for (int y = 0; y <= 7; y++)
            {
                for (int x = 0; x <= 7; x++)
                {
                    if (board[x, y] == 1)
                    {
                        e.Graphics.FillEllipse(Brushes.Black, x * 30 + 11, y * 30 + 11, 28, 28);
                    }
                    else if (board[x, y] == 2)
                    {
                        e.Graphics.FillEllipse(Brushes.White, x * 30 + 11, y * 30 + 11, 28, 28);
                    }
                }
            }
        }

        int player = 1;
        int rival = 2;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int x = (e.X - 10) / 30;
            int y = (e.Y - 10) / 30;
            if (Check(x, y, 0))
            {
                int put = 0;
                // 右
                if (Check(x + 1, y, rival) && Check(x + 2, y, player))
                {
                    board[x, y] = player;
                    board[x + 1, y] = player;
                    Refresh();
                    put = 1;
                }
                // 左
                if (Check(x - 1, y, rival) && Check(x - 2, y, player))
                {
                    board[x, y] = player;
                    board[x - 1, y] = player;
                    Refresh();
                    put = 1;
                }
                // 下
                if (Check(x, y + 1, rival) && Check(x, y + 2, player))
                {
                    board[x, y] = player;
                    board[x, y + 1] = player;
                    Refresh();
                    put = 1;
                }
                // 上
                if (Check(x, y - 1, rival) && Check(x, y - 2, player))
                {
                    board[x, y] = player;
                    board[x, y - 1] = player;
                    Refresh();
                    put = 1;
                }
                // 交替
                if (put == 1)
                {
                    int p = player;
                    player = rival;
                    rival = p;
                }
            }
        }

        private bool Check(int x, int y, int n)
        {
            return 0 <= x && x <= 7 && 0 <= y && y <= 7 && board[x, y] == n;
        }
    }
}
