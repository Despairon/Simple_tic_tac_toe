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

        private void graphicsInterface_MouseClick(object sender, MouseEventArgs e)
        {
            Tic_tac_toe.onClick(e.X, e.Y);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tic_tac_toe.new_game(graphicsInterface.Width, graphicsInterface.Height);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Tic_tac_toe.new_game(graphicsInterface.Width, graphicsInterface.Height);
        }
    }
}
