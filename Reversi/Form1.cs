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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(Pens.Black, 10, 10, 20, 20);
            e.Graphics.DrawRectangle(Pens.Black, 10, 10, 20, 20);
            e.Graphics.DrawEllipse(Pens.Blue, 10, 10, 20, 20);
            e.Graphics.FillRectangle(Brushes.Red, 40, 10, 20, 20);
        }
    }
}
