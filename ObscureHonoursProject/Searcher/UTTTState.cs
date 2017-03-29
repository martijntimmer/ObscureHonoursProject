using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    class UltimateTicTacToeState
    {
        int[,] field = new int[9, 9];
        int[,] macro = new int[3, 3];
        bool activePlayer;
        bool gameOver = false;

        // Generate state from given engine string
        // Generally always used to parse 
        UltimateTicTacToeState(String fieldString, String macroString)
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

            //Our turn
            activePlayer = true;
        }
        
        public void DoMove(UltimateTicTacToeMove move)
        {
            // Set to 1 / 2 depending on player
            field[move.x, move.y] = (activePlayer ? 1 : 2);
            // Check fields for winner

            // Check macro for winner

            // Alternate active player
            activePlayer ^= true;
        }

        public int Evaluate()
        {
            throw new NotImplementedException();
        }

        public List<Move> GetPossibleMoves()
        {
            throw new NotImplementedException();
        }

        public bool IsFinal()
        {
            return gameOver;
        }

        public bool MaximizingHasTurn()
        {
            return activePlayer;
        }

        public void UndoMove(Move move)
        {
            throw new NotImplementedException();
        }
    }
}
