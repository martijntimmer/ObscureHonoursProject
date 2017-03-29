
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    class ZobristHasher
    {
        // the board storing the randomly initialized integers for each cell
        private int[,,] board;

        // constructor intialized with seed
        public ZobristHasher(int horBoardDim, int verBoardDim, int pieceAmount, int seed) 
        {
            Random rand = new Random(seed);
            board = new int[horBoardDim, verBoardDim, pieceAmount];
            for (int i = 0; i < horBoardDim; i++)
            {
                for (int j = 0; j < verBoardDim; j++)
                {
                    for (int k = 0; k < pieceAmount; k++)
                    {
                        byte[] bytes = new byte[4];
                        rand.NextBytes(bytes);
                        board[i,j,k] = BitConverter.ToInt32(bytes, 0);
                    }
                }
            }
        }

        // constructor initialized with default seed value
        public ZobristHasher(int horBoardDim, int verBoardDim, int pieceAmount) : this(horBoardDim, verBoardDim, pieceAmount, 15)
        {
            // nothing left to do
        }

        // construct new hashcode based on target position and pieceID
        public int getNewHashcode(int originalCode, Tuple<int, int> targetPosition, int pieceID)
        {
            return originalCode ^ board[targetPosition.Item1, targetPosition.Item2, pieceID];
        }

        // gets the original hashcode, the actual value doesn't actually matter much...
        public int getOriginalHashcode ()
        {
            return 0;
        }
    }
}
