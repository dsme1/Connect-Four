using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public static class WinChecker
    {
        public static bool checkHorizontal = true;
        public static bool checkVertical = true;
        public static bool checkDiagonal = true;    
        
        public static bool CheckForWin(Colors[,] squares, Size boardSize, int winLength, ref int[] wonXPos, ref int[] wonYPos)
        {
            bool result = CheckHorizontal(squares, boardSize, winLength, ref wonXPos, ref wonYPos); 
            result |= CheckVertical(squares, boardSize, winLength, ref wonXPos, ref wonYPos);
            return result;
        }

        private static bool CheckHorizontal(Colors[,] squares, Size boardSize, int winLength, ref int[] wonXPos, ref int[] wonYPos)
        {
            if (!checkHorizontal)
            {
                return false;
            }

            for (int y = boardSize.Height -1; y >= 0; y--)
            {
                for (int x = 0; x <= boardSize.Width - winLength; x++)
                {
                    //isolates values from 2dimensional array
                    var color = squares[x, y];

                    //checks if squares are empty before continuing
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

        private static bool CheckVertical(Colors[,] squares, Size boardSize, int winLength, ref int[] wonXPos, ref int[] wonYPos)
        {
            if (!checkVertical)
            {
                return false;
            }

            for (int x = 0; x < boardSize.Width; x++)
            {
                for (int y = 0; y <= boardSize.Height - winLength; y++)
                {
                    var color = squares[x, y];

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
    }
}
