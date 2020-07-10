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
        Random random;

        //all initialisations when starting the game
        public ConnectFour()
        {
            InitializeComponent();
            board = new Board();
            gamePanel.Invalidate();
            random = new Random();
            playerTurn = random.Next()%2 == 0 ? Turn.red : Turn.yellow;
        }

        //draws the gamepanel in the window
        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {
            board.Draw(e.Graphics);
        }

        //handles on click events for placing pieces in the grid gamepanel
        private void gamePanel_MouseClick(object sender, MouseEventArgs e)
        {
            var succes = board.PlacePiece(e.Location, playerTurn == Turn.red ? Colors.red: Colors.yellow);

            if (succes)
            {
                if (board.isGameOver)
                {
                    ResetButton.Enabled = true;
                }
                playerTurn = playerTurn == Turn.red ? Turn.yellow : Turn.red;
            }
            //redraws after every click
            gamePanel.Invalidate();
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            board.Reset();
            gamePanel.Invalidate();
            ResetButton.Enabled = false;
            playerTurn = random.Next() % 2 == 0 ? Turn.red : Turn.yellow;
        }

        private enum Turn
        {
            red,
            yellow
        }

    }
}
