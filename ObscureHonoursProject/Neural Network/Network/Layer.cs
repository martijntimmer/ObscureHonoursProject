using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    class Layer
    {
        protected int numNodes;
        public Neuron[] nodes;
        public int WeightsPerNode;

        public int TotalWeights
        {
            get { return WeightsPerNode * nodes.Length; }
        }

        public Layer(Layer prevLayer, int nodes)
        {
            this.numNodes = nodes;
            createLayer(prevLayer);            
        }

        protected virtual void createLayer(Layer prevLayer)
        {
            nodes = new Neuron[numNodes];
            WeightsPerNode = prevLayer.nodes.Length + 1; // + 1 for bias
            for (int i = 0; i < numNodes; i++)
            {
                nodes[i] = new Neuron(prevLayer.nodes);
            }                        

        }

        public virtual void Update()
        {
            foreach (Neuron node in nodes)
            {
                node.Evaluate();
            }
        }

        public double[] GetValues()
        {
            double[] output = new double[nodes.Length];
            for (int i = 0; i < output.Length; i++)            
                output[i] = nodes[i].Value;
            return output;            
        }      
        
        public void Randomize()
        {
            foreach (Neuron node in nodes)
                node.RandomizeWeights();
        }

        public int WriteWeights(double[] output, int start)
        {
            for(int i = 0; i < nodes.Length; i++)            
                start = nodes[i].WriteWeights(output, start);
            
            return start;
        }
        public int UpdateWeights(double[] input, int start)
        {
            for (int i = 0; i < nodes.Length; i++)
                start = nodes[i].UpdateWeights(input, start);

            return start;
        }
    }
}
