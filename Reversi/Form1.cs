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
        public Form1()
        {
            InitializeComponent();
            Text = "リバーシ";
            ClientSize = new Size(260, 260);
            BackColor = Color.Green;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int i = 0; i <= 8; i++)
            {
                e.Graphics.DrawLine(Pens.Black, i * 30 + 10, 10, i * 30 + 10, 250);
                e.Graphics.DrawLine(Pens.Black, 10, i * 30 + 10, 250, i * 30 + 10);
            }
            e.Graphics.FillEllipse(Brushes.White, 101, 101, 28, 28);
            e.Graphics.FillEllipse(Brushes.Black, 131, 101, 28, 28);
            e.Graphics.FillEllipse(Brushes.Black, 101, 131, 28, 28);
            e.Graphics.FillEllipse(Brushes.White, 131, 131, 28, 28);
        }
    }
}
