using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    /// <summary>
    /// Class for checking if a winstate has been detected after a piece has been set. Returns a boolean value to Board class with every piece set
    /// </summary>
    public static class WinChecker
    {
        /// <summary>
        /// Booleans to check wether piece can be connected horizontally, vertically or diagonally
        /// </summary>
        public static bool checkHorizontal = true;
        public static bool checkVertical = true;
        public static bool checkDiagonal = true;

        /// <summary>
        /// eventhandler that runs the code after every piece being set
        /// </summary>
        /// <param name="squares"></param> The squares to check on the board
        /// <param name="boardSize"></param> The actual size of the board
        /// <param name="winLength"></param> The length required to win with
        /// <param name="wonXPos"></param> When a win is found, this contains the x-coordinates of the winning pieces
        /// <param name="wonYPos"></param> When a win is found, this contains the y-coordinates of the winning pieces
        /// <returns> A true value if a player connects the required winlength </returns>
        public static bool CheckForWin(Colors[,] squares, Size boardSize, int winLength, ref int[] wonXPos, ref int[] wonYPos)
        {
            bool result = CheckHorizontal(squares, boardSize, winLength, ref wonXPos, ref wonYPos);
            result |= CheckVertical(squares, boardSize, winLength, ref wonXPos, ref wonYPos);
            result |= CheckDiagonalRight(squares, boardSize, winLength, ref wonXPos, ref wonYPos);
            result |= CheckDiagonalLeft(squares, boardSize, winLength, ref wonXPos, ref wonYPos);
            return result;
        }

        //checks for horizontal win conditions
        private static bool CheckHorizontal(Colors[,] squares, Size boardSize, int winLength, ref int[] wonXPos, ref int[] wonYPos)
        {
            //returns if checkHorizontal is false stopping this codeblock from proceeding after a winstate is detected
            if (!checkHorizontal)
            {
                return false;
            }

            //nested for loops iterate over the squares array
            for (int y = boardSize.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x <= boardSize.Width - winLength; x++)
                {
                    //isolates values from 2dimensional array
                    var color = squares[x, y];

                    //checks if the squares array is filled with a color or not, if it is it continues the for loops
                    if (color == Colors.empty)
                    {
                        continue;
                    }

                    bool uninterrupted = true;

                    //checks squares for winlength
                    for (int length = 1; length < winLength; length++)
                    {
                        if (squares[x + length, y] != color)
                        {
                            uninterrupted = false;
                            break;
                        }
                    }

                    if (uninterrupted)
                    {
                        wonXPos = new int[winLength];
                        wonYPos = new int[winLength];

                        for (int i = 0; i < winLength; i++)
                        {
                            wonXPos[i] = x + i;
                            wonYPos[i] = y;
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        //checks for veritcal win conditions
        private static bool CheckVertical(Colors[,] squares, Size boardSize, int winLength, ref int[] wonXPos, ref int[] wonYPos)
        {
            //returns if checkVertical is false stopping this codeblock from proceeding after a winstate is detected
            if (!checkVertical)
            {
                return false;
            }

            //nested for loops iterate over the squares array
            for (int x = 0; x < boardSize.Width; x++)
            {
                for (int y = 0; y <= boardSize.Height - winLength; y++)
                {

                    //isolates values from 2dimensional array
                    var color = squares[x, y];

                    //checks if the squares array is filled with a color or not, if it is it continues the for loops
                    if (color == Colors.empty)
                    {
                        continue;
                    }

                    bool uninterrupted = true;

                    for (int length = 1; length < winLength; length++)
                    {
                        if (squares[x, y + length] != color)
                        {
                            uninterrupted = false;
                            break;
                        }
                    }

                    if (uninterrupted)
                    {
                        wonXPos = new int[winLength];
                        wonYPos = new int[winLength];

                        for (int i = 0; i < winLength; i++)
                        {
                            wonXPos[i] = x;
                            wonYPos[i] = y + i;
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CheckDiagonalRight(Colors[,] squares, Size boardSize, int winLength, ref int[] wonXPos, ref int[] wonYPos)
        {
            //returns if checkDiagonal is false stopping this codeblock from proceeding after a winstate is detected
            if (!checkDiagonal)
            {
                return false;
            }

            int x = 0, y = 0;

            for (; y <= boardSize.Height - winLength; y++)
            {
                for (int offset = 0; y + offset <= boardSize.Height - winLength; offset++)
                {
                    var current = squares[x + offset, y + offset];

                    if (current == Colors.empty)
                    {
                        continue;
                    }

                    bool uninterrupted = true;

                    for (int length = 1; length < winLength; length++)
                    {
                        if (squares[x + offset + length, y + offset + length] != current)
                        {
                            uninterrupted = false;
                            break;
                        }
                    }

                    if (uninterrupted)
                    {
                        wonXPos = new int[winLength];
                        wonYPos = new int[winLength];

                        for (int i = 0; i < winLength; i++)
                        {
                            wonXPos[i] = x + offset + i;
                            wonYPos[i] = y + offset + i;
                        }

                        return true;
                    }
                }
            }

            y = 0;
            x = 1;

            for (; x <= boardSize.Width - winLength; x++)
            {
                for (int offset = 0; x + offset <= boardSize.Width - winLength; offset++)
                {
                    var current = squares[x + offset, y + offset];

                    if (current == Colors.empty)
                    {
                        continue;
                    }

                    bool uninterrupted = true;

                    for (int length = 1; length < winLength; length++)
                    {
                        if (squares[x + offset + length, y + offset + length] != current)
                        {
                            uninterrupted = false;
                            break;
                        }
                    }

                    if (uninterrupted)
                    {
                        wonXPos = new int[winLength];
                        wonYPos = new int[winLength];

                        for (int i = 0; i < winLength; i++)
                        {
                            wonXPos[i] = x + offset + i;
                            wonYPos[i] = y + offset + i;
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CheckDiagonalLeft(Colors[,] squares, Size boardSize, int winLength, ref int[] wonXPos, ref int[] wonYPos)
        {
            //returns if checkDiagonal is false stopping this codeblock from proceeding after a winstate is detected
            if (!checkDiagonal)
            {
                return false;
            }

            int x = boardSize.Width - 1, y = 0;

            for (; y <= boardSize.Height - winLength; y++)
            {
                for (int offset = 0; y + offset <= boardSize.Height - winLength; offset++)
                {
                    var current = squares[x - offset, y + offset];

                    if (current == Colors.empty)
                    {
                        continue;
                    }

                    bool uninterrupted = true;

                    for (int length = 1; length < winLength; length++)
                    {
                        if (squares[x - offset - length, y + offset + length] != current)
                        {
                            uninterrupted = false;
                            break;
                        }
                    }

                    if (uninterrupted)
                    {
                        wonXPos = new int[winLength];
                        wonYPos = new int[winLength];

                        for (int i = 0; i < winLength; i++)
                        {
                            wonXPos[i] = x - offset - i;
                            wonYPos[i] = y + offset + i;
                        }

                        return true;
                    }
                }
            }

            y = 0;
            x = boardSize.Width - 2;

            for (; x > winLength -2; x--)
            {
                for (int offset = 0; x - offset > winLength; offset++)
                {
                    var current = squares[x - offset, y + offset];

                    if (current == Colors.empty)
                    {
                        continue;
                    }

                    bool uninterrupted = true;

                    for (int length = 1; length < winLength; length++)
                    {
                        if (squares[x - offset - length, y + offset + length] != current)
                        {
                            uninterrupted = false;
                            break;
                        }
                    }

                    if (uninterrupted)
                    {
                        wonXPos = new int[winLength];
                        wonYPos = new int[winLength];

                        for (int i = 0; i < winLength; i++)
                        {
                            wonXPos[i] = x - offset - i;
                            wonYPos[i] = y + offset + i;
                        }

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
