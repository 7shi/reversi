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
                put = PutStone(x, y, 1, 0, put);   // 右
                put = PutStone(x, y, -1, 0, put);  // 左
                put = PutStone(x, y, 0, 1, put);   // 下
                put = PutStone(x, y, 0, -1, put);  // 上
                put = PutStone(x, y, 1, 1, put);   // 右下
                put = PutStone(x, y, 1, -1, put);  // 右上
                put = PutStone(x, y, -1, 1, put);  // 左下
                put = PutStone(x, y, -1, -1, put); // 左上
                // 交替
                if (put == 1)
                {
                    board[x, y] = player;
                    int p = player;
                    player = rival;
                    rival = p;
                    Refresh();
                }
            }
        }

        private int PutStone(int x, int y, int dx, int dy, int put)
        {
            int x1 = x + dx;
            int y1 = y + dy;
            int stone = 0;
            while (Check(x1, y1, rival))
            {
                x1 += dx;
                y1 += dy;
                stone++;
            }
            if (stone > 0 && Check(x1, y1, player))
            {
                int x2 = x + dx;
                int y2 = y + dy;
                while (Check(x2, y2, rival))
                {
                    board[x2, y2] = player;
                    x2 += dx;
                    y2 += dy;
                }
                put = 1;
            }
            return put;
        }

        private bool Check(int x, int y, int n)
        {
            return 0 <= x && x <= 7 && 0 <= y && y <= 7 && board[x, y] == n;
        }
    }
}
