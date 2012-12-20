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
        int[,] board;
        string message;

        private void Init()
        {
            message = "";
            board = new int[8, 8];
            board[3, 3] = 2;
            board[4, 3] = 1;
            board[3, 4] = 1;
            board[4, 4] = 2;
            CountStones();
        }

        public Form1()
        {
            InitializeComponent();
            Text = "リバーシ";
            ClientSize = new Size(300, 260);
            BackColor = Color.Green;
            Init();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int y = 0; y <= 7; y++)
            {
                for (int x = 0; x <= 7; x++)
                {
                    if (CountStone(x, y) > 0)
                    {
                        e.Graphics.FillRectangle(Brushes.Yellow,
                            x * 30 + 10, y * 30 + 10, 30, 30);
                    }
                    DrawStone(e, board[x, y], x * 30 + 11, y * 30 + 11);
                }
            }
            for (int i = 0; i <= 8; i++)
            {
                e.Graphics.DrawLine(Pens.Black, i * 30 + 10, 10, i * 30 + 10, 250);
                e.Graphics.DrawLine(Pens.Black, 10, i * 30 + 10, 250, i * 30 + 10);
            }
            var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            e.Graphics.DrawString("Turn", Font, Brushes.White,
                new Rectangle(250, 230, 50, 30), sf);
            DrawStone(e, player, 261, 201);
            DrawStone(e, 1, 261, 11);
            e.Graphics.DrawString(black.ToString(), Font, Brushes.Black,
                new Rectangle(250, 40, 50, 30), sf);
            DrawStone(e, 2, 261, 71);
            e.Graphics.DrawString(white.ToString(), Font, Brushes.White,
                new Rectangle(250, 100, 50, 30), sf);
            if (message != "")
            {
                var r = new Rectangle(20, 120, 220, 20);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Red, r);
                e.Graphics.DrawString(message, Font, Brushes.Black, r, sf);
            }
        }

        private static void DrawStone(PaintEventArgs e, int n, int x, int y)
        {
            if (n == 1)
            {
                e.Graphics.FillEllipse(Brushes.Black, x, y, 28, 28);
            }
            else if (n == 2)
            {
                e.Graphics.FillEllipse(Brushes.White, x, y, 28, 28);
            }
        }

        int player = 1;
        int rival = 2;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int x = (e.X - 10) / 30;
            int y = (e.Y - 10) / 30;
            if (message == "")
            {
                if (Check(x, y, 0))
                {
                    int put = 0;
                    put += PutStone(x, y, 1, 0);   // 右
                    put += PutStone(x, y, -1, 0);  // 左
                    put += PutStone(x, y, 0, 1);   // 下
                    put += PutStone(x, y, 0, -1);  // 上
                    put += PutStone(x, y, 1, 1);   // 右下
                    put += PutStone(x, y, 1, -1);  // 右上
                    put += PutStone(x, y, -1, 1);  // 左下
                    put += PutStone(x, y, -1, -1); // 左上
                    // 交替
                    if (put > 0)
                    {
                        board[x, y] = player;
                        CountStones();
                        int p = player;
                        player = rival;
                        rival = p;
                        if (!CanPut())
                        {
                            rival = player;
                            player = p;
                            if (!CanPut())
                            {
                                if (black > white)
                                {
                                    message = "黒の勝ち！";
                                }
                                else if (black < white)
                                {
                                    message = "白の勝ち！";
                                }
                                else
                                {
                                    message = "引き分け！";
                                }
                            }
                        }
                        Refresh();
                    }
                }
            }
            else
            {
                Init();
                Refresh();
            }
        }

        private bool CanPut()
        {
            for (int y = 0; y <= 7; y++)
            {
                for (int x = 0; x <= 7; x++)
                {
                    if (CountStone(x, y) > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private int PutStone(int x, int y, int dx, int dy)
        {
            int stone = CountStone(x, y, dx, dy);
            for (int i = 1; i <= stone; i++)
            {
                board[x + dx * i, y + dy * i] = player;
            }
            return stone;
        }

        private int CountStone(int x, int y)
        {
            int stone = 0;
            if (Check(x, y, 0))
            {
                stone += CountStone(x, y, 1, 0);   // 右
                stone += CountStone(x, y, -1, 0);  // 左
                stone += CountStone(x, y, 0, 1);   // 下
                stone += CountStone(x, y, 0, -1);  // 上
                stone += CountStone(x, y, 1, 1);   // 右下
                stone += CountStone(x, y, 1, -1);  // 右上
                stone += CountStone(x, y, -1, 1);  // 左下
                stone += CountStone(x, y, -1, -1); // 左上
            }
            return stone;
        }

        private int CountStone(int x, int y, int dx, int dy)
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
            if (!Check(x1, y1, player))
            {
                stone = 0;
            }
            return stone;
        }

        private bool Check(int x, int y, int n)
        {
            return 0 <= x && x <= 7 && 0 <= y && y <= 7 && board[x, y] == n;
        }

        int black, white;

        private void CountStones()
        {
            black = 0;
            white = 0;
            for (int y = 0; y <= 7; y++)
            {
                for (int x = 0; x <= 7; x++)
                {
                    if (board[x, y] == 1)
                    {
                        black++;
                    }
                    else if (board[x, y] == 2)
                    {
                        white++;
                    }
                }
            }
        }
    }
}
