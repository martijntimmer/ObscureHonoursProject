using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    class UTTTState
    {
        int[,] field = new int[9, 9];
        int[,] macro = new int[3, 3];
        UTTTMove[,] moveArray = new UTTTMove[9, 9];
        bool activePlayer; // our bot active <=> activePlayer == true
        bool gameOver = false;

        ZobristHasher zobristHasher;
        private int hashCode;

        public override int GetHashCode()
        {
            return hashCode;
        }

        // Generate state from given engine string
        // Generally always used to parse 
        public UTTTState(String fieldString, String macroString, ZobristHasher zobristHasher, bool weArePlayerOne)
        {
            String[] fieldArray = fieldString.Split(',');
            for (int i = 0; i != 81; i++)
            {
                field[i % 9, i / 9] = Int32.Parse(fieldArray[i]);
            }

            String[] macroArray = macroString.Split(',');
            for (int i = 0; i != 9; i++)
            {
                macro[i % 3, i / 3] = Int32.Parse(macroArray[i]);
            }

            for (int i = 0; i != 81; i++)
            {
                moveArray[i % 9, i / 9] = new UTTTMove(i % 9, i / 9);
            }

            //Our turn
            activePlayer = weArePlayerOne;
            this.zobristHasher = zobristHasher;
            this.hashCode = zobristHasher.getOriginalHashcode();
        }


        public void UndoMove(UTTTMove move)
        {
            field[move.x, move.y] = 0;
            hashCode = zobristHasher.getNewHashcode(hashCode, new Tuple<int,int> (move.x, move.y), activePlayer ? 0 : 1);
            UpdateState(move);
        }

        public void DoMove(UTTTMove move)
        {
            // Set to 1 / 2 depending on player
            field[move.x, move.y] = (activePlayer ? 1 : 2);
            hashCode = zobristHasher.getNewHashcode(hashCode, new Tuple<int, int>(move.x, move.y), activePlayer ? 1 : 0);
            UpdateState(move);
        }

        public void UpdateState(UTTTMove move)
        {
            // Check fields for winner
            CheckField(move.x / 3 + move.y / 3 * 3);

            // Check macro for winner
            CheckMacro();

            // Alternate active player
            activePlayer ^= true;
        }

        private int CheckField(int fieldNum)
        {
            int x = fieldNum % 3 * 3;
            int y = fieldNum / 3 * 3;
            //vertical rows
            for ( int i = x; i != x+3; i++)
            {
                if (field[i, y] == field[i, y + 1] && field[i, y + 1] == field[i, y + 2])
                {
                    return field[i, y];
                }
            }
            //horizontal rows
            for (int i = y; i != y + 3; i++)
            {
                if (field[x, i] == field[x + 1, i] && field[x + 1, i] == field[x + 2, i])
                {
                    return field[x, i];
                }
            }
            //diagonals
            if (field[x, y] == field[x + 1, y + 1] && field[x + 1, y + 1] == field[x + 2, y + 2])
            {
                return field[x, y];
            }
            if (field[x, y + 2] == field[x + 1, y + 1] && field[x + 1, y + 1] == field[x + 2, y])
            {
                return field[x, y + 2];
            }

            return 0;
        }
        private void CheckMacro()
        {
            //vertical rows
            for (int i = 0; i != 3; i++)
            {
                if ((field[i, 0] == field[i, 1] && field[i, 1] == field[i, 2])
                 || (field[0, i] == field[1, i] && field[1, i] == field[2, i]))
                {
                    gameOver = true;
                    return;
                }
            }
            //diagonals
            if (field[0, 0] == field[1,  1] && field[ 1,  1] == field[ 2,  2])
            {
                gameOver = true;
                return;
            }
            if (field[0, 2] == field[ 1,  1] && field[ 1, 1] == field[ 2, 0])
            {
                gameOver = true;
                return;
            }

            gameOver = false;
            return;
        }

        public int Evaluate()
        {
            // 1 is our bot
            // 2 is him bot
            throw new NotImplementedException();
        }

        public List<UTTTMove> GetPossibleMoves()
        {
            List<UTTTMove> result = new List<UTTTMove>();
            for (int i = 0; i != 9; i++)
            {
                if ( macro[i % 3, i / 3] == -1)
                {
                    int xStart = i % 3 * 3;
                    int yStart = i / 3 * 3;
                    for (int x = xStart; x != xStart+3; x++)
                    {
                        for (int y = yStart; y != yStart + 3; y++)
                        {
                            if (field[x, y] == 0)
                            {
                                result.Add(moveArray[x, y]);
                            }
                        }
                    }
                } 
            }
            return result;
        }

        public bool IsFinal()
        {
            return gameOver;
        }

        public bool MinimizingHasTurn()
        {
            return !activePlayer;
        }
    }
}
