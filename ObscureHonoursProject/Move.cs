using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    // A move that acts as a transition between States
    // When extending, make sure to Equate to implementation so implementation-
    // specific fields can be compared.
    interface Move : IEquatable<Move>
    {

        // Returns string representation of the move
        String ToString();
    }
}
