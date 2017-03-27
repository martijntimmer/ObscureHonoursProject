using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    /* Interface for a mutable state to be used for alpha-beta search
     * Assumes that this is a two-player sequential game with a minimizing and maximizing player
     */
    interface State
    {
        // return a list of possible moves from this state
        // undefined if IsFinal()
        List<Move> GetPossibleMoves();

        // executes move
        // modifies this
        void DoMove(Move move);

        // undoes a move
        // modifies this
        void UndoMove(Move move);

        // returns whether this state is final (no moves can be made anymore)
        bool IsFinal();

        // returns evaluation of this state
        // maybe evaluate within alphabeta instead of here.
        int Evaluate();

        // returns whether the 'minimizing player' has the turn in this state
        bool MinimizingHasTurn();
    }
}
