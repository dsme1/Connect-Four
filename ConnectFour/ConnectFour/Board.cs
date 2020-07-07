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
        private int columns;
        private int rows;
        private Colors[,] squares;

        public Board(int columns = 7, int rows = 6)
        {
            this.columns = columns;
            this.rows = rows;
            squares = new Colors[columns, rows];
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    squares[column, row] = Colors.empty;
                }
            }
        }

        public bool PlacePiece(Point location, Colors piece)
        {
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
            return true;
        }

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

            for ( int column = 0; column <= columns; column++)
            {
                g.DrawLine(Pens.White, column * squareSize, 0 ,column * squareSize, rows * squareSize);
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
