using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    class UltimateTicTacToeState : State
    {
        int[,] field = new int[9, 9];
        int[,] macroboard = new int[3, 3];
        bool activePlayer;

        public void DoMove(Move move)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool MinimizingHasTurn()
        {
            throw new NotImplementedException();
        }

        public void UndoMove(Move move)
        {
            throw new NotImplementedException();
        }
    }
}
