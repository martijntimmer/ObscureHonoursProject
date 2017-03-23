using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    // A class that implements iterative deepening which makes use of alpha-beta pruned minimax search
    // to find the best move for the player which currently has the turn with time as main constraint
    // Ends slightly later (at leat 1ms) than given, as needs to wrap up search 
    class TimedSearcher
    {
        // Returns the best move from some particular state for the player which has the turn, within the time given
        // Returns random move if not enough time is given
        // pre: startState != null && !startState.isFinal()
        public Move FindBestMove(State startState, int msGiven)
        {
            if (startState == null || startState.IsFinal())
            {
                throw new ArgumentException("TimedSearcher.FindBestMove: Invalid startState passed.");
            }

            // stopwatch keeps track of how much time we have left
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // iterative deepening
            Move bestMove = null;
            List<Move> oldMoveList = new List<Move>();
            TimedAlphaBeta timedAB = new TimedAlphaBeta(sw, msGiven);

            for (int depthLeft = 1; sw.ElapsedMilliseconds < msGiven; depthLeft++)
            {
                List<Move> newMoveList = new List<Move>();
                timedAB.FindBestMove(startState, int.MinValue, int.MaxValue, depthLeft, oldMoveList, newMoveList);
                if ( !(newMoveList.Count == 0) )
                {
                    newMoveList.Reverse();
                    bestMove = newMoveList.First();
                    oldMoveList = newMoveList;
                }
                else break;
            }

            // if no time to find any move, return random move
            if (bestMove == null)
            {
                bestMove = startState.GetPossibleMoves().First();
            }

            return bestMove;
        }
    }
}
