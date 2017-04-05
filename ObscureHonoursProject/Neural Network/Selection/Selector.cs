using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    interface Selector
    {
        List<NeuralNetwork> Select(Population population, int amount);
    }
}
