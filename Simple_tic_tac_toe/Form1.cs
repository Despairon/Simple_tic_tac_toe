using System;
using System.Windows.Forms;

namespace Simple_tic_tac_toe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Render.init(ref graphicsInterface);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void renderTimer_Tick(object sender, EventArgs e)
        {
            Render.drawAll();
        }
    }
}
