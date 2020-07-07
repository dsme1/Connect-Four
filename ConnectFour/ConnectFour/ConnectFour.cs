using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class ConnectFour : Form
    {
        Board board;
        Turn playerTurn;

        public ConnectFour()
        {
            InitializeComponent();
            board = new Board();
            gamePanel.Invalidate();
            playerTurn = Turn.red;
        }

        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {
            board.Draw(e.Graphics);
        }

        private void gamePanel_MouseClick(object sender, MouseEventArgs e)
        {
            var succes = board.PlacePiece(e.Location, playerTurn == Turn.red ? Colors.red: Colors.yellow);
            if (succes)
            {
                playerTurn = playerTurn == Turn.red ? Turn.yellow : Turn.red;
            }
            gamePanel.Invalidate();
        }

        private enum Turn
        {
            red,
            yellow
        }
    }
}
