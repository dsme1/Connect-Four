using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class Board
    {
        public const int squareSize = 60;
        private Size boardSize;
        public int columns => boardSize.Width;
        public int rows => boardSize.Height;
        private Colors[,] squares;
        public bool isGameOver { get; private set; }
        private int[] wonXPos;
        private int[] wonYPos;
        public int winLength { get; private set; }

        //gives values for the amount of rows and columns to draw
        public Board(int columns = 7, int rows = 6, int winLength = 4)
        {
            this.winLength = winLength;
            boardSize = new Size(columns, rows);
            Reset();
        }

        public void Reset()
        {
            squares = new Colors[columns, rows];
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    //makes sure that when drawing the board for the first time all the squares are empty
                    squares[column, row] = Colors.empty;
                }
            }
            isGameOver = false;
        }

        //places the piece
        public bool PlacePiece(Point location, Colors piece)
        {
            if (isGameOver)
            {
                return false;
            }

            int column = location.X / squareSize;

            if (column == columns)
            {
                column--;
            }

            int row = ProbeY(column);

            if (row == -1)
            {
                return false;
            }
            squares[column, row] = piece;

            isGameOver = WinChecker.CheckForWin(squares, boardSize, winLength, ref wonXPos, ref wonYPos);

            return true;
        }

        //checks availability of the row placing the piece at the bottom if possible to place
        private int ProbeY(int column)
        {
            int availableY = -1;
            for (int y = 0; y < rows; y++)
            {
                if (squares[column, y] == Colors.empty)
                {
                    availableY = y;
                }
                else
                {
                    break;
                }
            }
            return availableY;
        }

        //draws the actual gaming board
        public void Draw(Graphics g)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (squares[column, row] == Colors.empty)
                    {
                        continue;
                    }

                    var brush = Brushes.Red;
                    if (squares[column, row] == Colors.yellow)
                    {
                        brush = Brushes.Yellow;
                    }

                    g.FillEllipse(brush, column * squareSize, row * squareSize, squareSize, squareSize);
                }
            }

            for (int row = 0; row <= rows; row++)
            {
                g.DrawLine(Pens.White, 0, row * squareSize, columns * squareSize, row * squareSize);
            }

            for (int column = 0; column <= columns; column++)
            {
                g.DrawLine(Pens.White, column * squareSize, 0, column * squareSize, rows * squareSize);
            }

            if (isGameOver)
            {
                Size dotSize = new Size((int)(squareSize * 0.2f), (int)(squareSize * 0.2f));
                Point location;

                for (int i = 0; i < winLength; i++)
                {
                    location = new Point
                    {
                        X = wonXPos[i] * squareSize + (int)(squareSize * 0.4f),
                        Y = wonYPos[i] * squareSize + (int)(squareSize * 0.4f)
                    };

                    g.FillEllipse(Brushes.White, new Rectangle(location, dotSize));
                }

                using Pen pen = new Pen(Color.White, squareSize / 15.0f);
                location = new Point
                {
                    X = wonXPos[0] * squareSize + (int)(squareSize * 0.5f),
                    Y = wonYPos[0] * squareSize + (int)(squareSize * 0.5f)
                };

                Point locationTwo = new Point
                {
                    X = wonXPos[winLength - 1] * squareSize + (int)(squareSize * 0.5f),
                    Y = wonYPos[winLength - 1] * squareSize + (int)(squareSize * 0.5f)
                };

                g.DrawLine(pen, location, locationTwo);
            }
        }
    }

    public enum Colors
    {
        empty = 0,
        yellow,
        red
    }
}
