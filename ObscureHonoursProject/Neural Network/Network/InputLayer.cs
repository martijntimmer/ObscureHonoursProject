using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    class InputLayer : Layer
    {
        
        public InputLayer(int nodes)
            : base(null, nodes)
        {

        }

        protected override void createLayer(Layer prevLayer)
        {
            nodes = new Neuron[numNodes];
            for (int i = 0; i < numNodes; i++)
            {
                nodes[i] = new Neuron();
            }
        }

        public override void Update()
        {
            throw new Exception("Can not update an input layer");
        }

        public void SetValues(double[] vals)
        {
            for (int i = 0; i < vals.Length; i++)
                nodes[i].Value = vals[i];
        }
    }
}
