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

        // initialize these objects once and re-use them in every node of AlphaBeta to save time
        List<Move> bestChildList;
        List<Move> candidateChildList;

        public TimedAlphaBeta(Stopwatch sw, int msGiven)
        {
            this.sw = sw;
            this.msGiven = msGiven;
            bestChildList = new List<Move>();
            candidateChildList = new List<Move>();
        }

        // state        : the root of the (sub)-tree this search expands
        // alpha        : Value used for AlphaBeta pruning - maximum value maximizing player can definitely get
        // beta         : Value used for AlphaBeta pruning - minimum value minimizing player can definitely get
        // depthLeft    : How many more depths we are allowed to explore
        // oldMoveList  : Optimal moves of previous iteration, used for better pruning
        // newMoveList  : OUTPUT list of moves WE have to populate, at the end of the root-call it must contain
        //      the optimal sequence of moves REVERSED, appended to the \old(newMoveList)
        public int FindBestMove (State state, int alpha, int beta, int depthLeft, List<Move> oldMoveList, List<Move> newMoveList)
        {
            // throw an exception if our player is forced to stop
            if (sw.ElapsedMilliseconds > msGiven)
            {
                newMoveList = null;
                return 0;
            }

            // if final depth reached, then return the value of this leaf
            if (depthLeft == 0 || state.IsFinal())
            {
                return state.Evaluate();
            }

            // if not final depth, then generate all possible branches
            // determine the best move and corresponding state-value
            List<Move> moves = state.GetPossibleMoves();

            // if only a single move is possible, don't decrease depth
            if (moves.Count == 1)
            {
                depthLeft++;
            }

            // if the list of old-moves is not empty yet, then continue
            // tracing the best move of the previous iteration by putting the old move
            // in the first position to be evaluated. This leads to better pruning.
            if ( oldMoveList.Count != 0 )
            {
                Move oldMove = oldMoveList.First();
                oldMoveList.RemoveAt(0);
                // Is this really less efficient than just processing the oldMove first?
                // Especially if we properly implement hashing this should be the fastest method.
                // moves.Insert(0, oldMove);
                FindCopyAndSwap(oldMove, moves, moves.First());
            }

            // keep track of the moveList of the best move
            // combine it with the input moveList to get full move-history
            Boolean min = state.MinimizingHasTurn();
/* !!!!!! */int res = 0; 
            foreach (Move move in moves)
            {
                state.DoMove(move);
                int result = FindBestMove(state, alpha, beta, depthLeft - 1, oldMoveList, candidateChildList);
                state.UndoMove(move);
                if ((min && result < beta) || (!min && result > alpha) )
                {
                    if (min)
                        beta = result;
                    else
                        alpha = result;
                    List<Move> temp = candidateChildList;
                    candidateChildList = bestChildList; // Does this copy the list?
                    bestChildList = temp;
                    bestChildList.Add(move);
                }
                candidateChildList.Clear(); // Why do we clear it?
                if (alpha >= beta)
                {
                    // append child move list to move list
                    newMoveList.AddRange(bestChildList);
                    bestChildList.Clear();
                    return (alpha+beta)/2;
                }
            }
            // append child move list to move list
            newMoveList.AddRange(bestChildList);
            bestChildList.Clear();
            if (min)
                return beta;
            else
                return alpha;
        }

        // Finds a copy of a Move in a list of Moves, then swaps the found copy with another given move
        void FindCopyAndSwap(Move toFind, List<Move> moves, Move toSwapWith)
        {
            Move moveCopy = null;
            foreach (Move move in moves)
            {
                if (move.Equals(toFind))
                {
                    moveCopy = move;
                    break;
                }
            }
            toFind = toSwapWith;
            toSwapWith = moveCopy;
        }
    }
}
