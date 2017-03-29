using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    /// <summary>
    /// Implements a time-constrained Alpha-Beta that keeps track of intermediate results
    /// in a dictionary by hashing.
    /// </summary>
    class TimedAlphaBetaWIP
    {
        // stores a stopwatch and milliseconds given to bound the time it takes
        private Stopwatch sw;
        private int msGiven;

        // initialize these objects once and re-use them in every node of AlphaBeta to save time
        private Dictionary<int, StateRecord> statesSeen;
        private int iteration;
        private UTTTState startState;

        // stores information about states that have been processed
        private class StateRecord
        {
            public double value;
            public UTTTMove bestMove;
            public int iteration;

            public void Update(double value, UTTTMove bestMove, int iteration)
            {
                this.value = value;
                this.bestMove = bestMove;
                this.iteration = iteration;
            }
        }

        public TimedAlphaBetaWIP(Stopwatch sw, int msGiven, UTTTState startState)
        {
            this.sw = sw;
            this.msGiven = msGiven;
            this.statesSeen = new Dictionary<int, StateRecord>();
            this.startState = startState;
            this.iteration = 0;
        }

        // gets best move AFTER an iteration has been completed
        private UTTTMove GetBestMove()
        {
            StateRecord rec = new StateRecord();
            statesSeen.TryGetValue(startState.GetHashCode(), out rec);
            return rec.bestMove;
        }

        /// <summary>
        /// Stores the result of a single Alpha-Beta iterative deepening iteration
        /// </summary>
        public struct AlphaBetaIterationResult
        {
            public bool outOfTime;
            public int iteration;
            public UTTTMove bestMove;
            public double bestMoveValue;
        }

        /// <summary>
        /// Execute the next iteration in the iterative-deepening process.
        /// </summary>
        /// <returns> An AlphaBetaIterationResult struct containing the results </returns>
        public AlphaBetaIterationResult computeNextIteration()
        {
            iteration++;
            AlphaBetaIterationResult res = new AlphaBetaIterationResult();
            try {
                res.bestMoveValue = AlphaBetaSearch(startState, Double.NegativeInfinity, Double.PositiveInfinity, iteration);
                res.iteration = iteration;
                res.outOfTime = false;
                res.bestMove = GetBestMove();
            }
            catch
            {
                res.bestMoveValue = 0;
                res.bestMove = null;
                res.outOfTime = true;
                iteration--;
                res.iteration = iteration;
            }
            return res;
        }

        private class OutOfTimeException : Exception { };

        // state        : The root of the (sub)-tree this search expands
        // alpha        : Value used for AlphaBeta pruning - maximum value maximizing player can definitely get
        // beta         : Value used for AlphaBeta pruning - minimum value minimizing player can definitely get
        // depthLeft    : How many more depths we are allowed to explore
        private double AlphaBetaSearch  (UTTTState state, double alpha, double beta, int depthLeft)
        {
            // throw an exception if our player is forced to stop
            if (sw.ElapsedMilliseconds > msGiven)
            {
                throw new OutOfTimeException();
            }

            // if final depth reached, then return the value of this leaf
            if (depthLeft == 0 || state.IsFinal())
            {
                return state.Evaluate();
            }

            // if not final depth, then generate all possible branches
            List<UTTTMove> moves = state.GetPossibleMoves();

            // if only a single move is possible, don't decrease depth
            if (moves.Count == 1)
            {
                depthLeft++;
            }

            // if the state has been seen before, then continue
            // tracing the best move of the previous iteration by putting the old move
            // in the first position to be evaluated. This leads to better pruning.
            StateRecord rec = null;
            statesSeen.TryGetValue(state.GetHashCode(), out rec);
            if ( rec != null )
            {
                if (rec.iteration == iteration)
                {
                    return rec.value;
                }
                FindCopyAndSwap(rec.bestMove, moves, moves.First());
            }

            // keep track of the best move
            // update the StateRecord in the dictionary after we have finished analyzing the state
            Boolean min = state.MinimizingHasTurn();
            UTTTMove bestMove = null;
            foreach (UTTTMove move in moves)
            {
                state.DoMove(move);
                double result = AlphaBetaSearch(state, alpha, beta, depthLeft - 1);
                state.UndoMove(move);
                if ((min && result < beta) || (!min && result > alpha) )
                {
                    bestMove = move;
                    if (min)
                        beta = result;
                    else
                        alpha = result;
                }
                if (alpha >= beta)
                {
                    // update record and return
                    double res = (alpha + beta) / 2;
                    rec.Update(res, bestMove, iteration);
                    return res;
                }
            }
            // update record and return
            if (min)
            {
                rec.Update(beta, bestMove, iteration);
                return beta;
            }
            else
            {
                rec.Update(alpha, bestMove, iteration);
                return alpha;
            }
        }

        // Finds a copy of a Move in a list of Moves, then swaps the found copy with another given move
        private void FindCopyAndSwap(UTTTMove toFind, List<UTTTMove> moves, UTTTMove toSwapWith)
        {
            UTTTMove moveCopy = null;
            foreach (UTTTMove move in moves)
            {
                if (move.Equals(toFind))
                {
                    moveCopy = move;
                    break;
                }
            }
            if (moveCopy == null)
                return;
            toFind = toSwapWith;
            toSwapWith = moveCopy;
        }
    }
}
