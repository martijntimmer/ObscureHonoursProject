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
        bool activePlayer;
        bool gameOver = false;

        // Generate state from given engine string
        // Generally always used to parse 
        UTTTState(String fieldString, String macroString)
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
        
        public void DoMove(UTTTMove move)
        {
            // Set to 1 / 2 depending on player
            field[move.x, move.y] = (activePlayer ? 1 : 2);
            // Check fields for winner

            // Check macro for winner

            // Alternate active player
            activePlayer ^= true;
        }

        public void UndoMove(UTTTMove move)
        {
            throw new NotImplementedException();
        }

        public int Evaluate()
        {
            throw new NotImplementedException();
        }

        public List<UTTTMove> GetPossibleMoves()
        {
            throw new NotImplementedException();
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
