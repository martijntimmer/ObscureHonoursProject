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
    class TimedSearcherWIP
    {
        // Returns the best move from some particular state for the player which has the turn, within the time given
        // Returns random move if not enough time is given
        // pre: startState != null && !startState.isFinal()
        public UTTTMove FindBestMove(UTTTState startState, int msGiven)
        {
            if (startState == null || startState.IsFinal())
            {
                throw new ArgumentException("TimedSearcher.FindBestMove: Invalid startState passed.");
            }

            // stopwatch keeps track of how much time we have left
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // iterative deepening
            UTTTMove bestMove = null;
            TimedAlphaBetaWIP timedAB = new TimedAlphaBetaWIP(sw, msGiven, startState);

            for (int depthLeft = 1; sw.ElapsedMilliseconds < msGiven; depthLeft++)
            {
                List<UTTTMove> newMoveList = new List<UTTTMove>();
                TimedAlphaBetaWIP.AlphaBetaIterationResult res = timedAB.computeNextIteration();
                if (res.outOfTime)
                    break;
                else
                {
                    bestMove = res.bestMove;
                }
            }

            // if no time to find any move, return random move
            if (bestMove == null)
            {
                bestMove = startState.GetPossibleMoves().First();
            }

            sw.Stop();
            return bestMove;
        }
    }
}
