using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    class UltimateTicTacToeMove : Move
    {
        // In Ultimate Tic Tac Toe a move is simply defined by an x and a y.
        // So without further ado:
        private int x, y;

        UltimateTicTacToeMove(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        override public string ToString()
        {
            return this.x + " " + this.y;
        }

        public string MakeMoveString()
        {
            return "place_move " + this.ToString();
        }

        public bool Equals(UltimateTicTacToeMove other)
        {
            return (this.x == other.x && this.y == other.y);
        }
    }
}
