using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    interface Crossover
    {
        double[][] Cross(double[] weightsP1, double[] weightsP2);
    }
}