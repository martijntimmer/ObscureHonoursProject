using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    // a class that implements a time-constrained Alpha-Beta that keeps track of optimal
    // Move-lists for better pruning in future iterative deepening iterations
    class TimedAlphaBeta
    {
        // stores a stopwatch and milliseconds given to bound the time it takes
        private Stopwatch sw;
        private int msGiven;

        public TimedAlphaBeta(Stopwatch sw, int msGiven)
        {
            this.sw = sw;
            this.msGiven = msGiven;
        }

        // state: the root of the (sub)-tree this search expands
        // alpha: Value used for AlphaBeta pruning - maximum value maximizing player can definitely get
        // beta: Value used for AlphaBeta pruning - minimum value minimizing player can definitely get
        // depthLeft: How many more depths we are allowed to explore
        // oldMoveList: Optimal moves of previous iteration, used for better pruning
        // newMoveList: OUTPUT list of moves WE have to populate, at the end of the root-call it must contain
        //      the optimal sequence of moves REVERSED, appended to the \old(newMoveList)
        public Move FindBestMove (State state, int alpha, int beta, int depthLeft, List<Move> odMoveList, List<Move> newMoveList)
        {
            // TODO: implement alphabeta stuffs
            return null;
        }
    }
}
