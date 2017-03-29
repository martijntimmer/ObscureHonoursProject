using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    class NeuralNetwork
    {
        private InputLayer inputLayer;
        private List<Layer> hiddenLayer;
        private Layer outputLayer;
        private List<Layer> layers;
        public double Fitness;
        const int HIDDEN_NODES = 2;
        double[] weights;

        int numWeights = -1;
        public int NumWeights
        {
            get
            {
                if (numWeights != -1)
                    return numWeights;
                numWeights = 0;
                foreach (Layer layer in layers)
                    numWeights += layer.WeightsPerNode * layer.nodes.Length;
                return numWeights;
            }
        }

        public NeuralNetwork(int numInputs, int numOutputs, int numHidden = 1)
        {
            inputLayer = new InputLayer(numInputs);
            hiddenLayer.Add(new Layer(inputLayer, HIDDEN_NODES));
            for (int i = 1; i < numHidden; i++)
            hiddenLayer.Add(new Layer(hiddenLayer[i-1], HIDDEN_NODES));
            outputLayer = new Layer(hiddenLayer[numHidden-1], numOutputs);

            layers = new List<Layer>();
            layers.Add(inputLayer);
            layers.AddRange()
            layers.Add(outputLayer);
        }

        public NeuralNetwork(NeuralNetwork original)
        {
            inputLayer = new InputLayer(original.inputLayer.nodes.Length);
            hiddenLayer = new Layer(inputLayer, HIDDEN_NODES);
            outputLayer = new Layer(hiddenLayer, original.outputLayer.nodes.Length);

            layers = new List<Layer>();
            layers.Add(inputLayer);
            layers.Add(hiddenLayer);
            layers.Add(outputLayer);
        }

        public double[] Evaluate(int[] input)
        {
            inputLayer.SetValues(input);
            hiddenLayer.Update();
            outputLayer.Update();
            return outputLayer.GetValues();
        }

        public double[] WriteWeights()
        {
            if (weights != null)
                return weights;
            weights = new double[NumWeights];
            int start = 0;
            foreach(Layer layer in layers)
                start = layer.WriteWeights(weights, start);
            return weights;
        }

        public int UpdateWeights(double[] input, int start)
        {
            foreach (Layer layer in layers)
                start = layer.UpdateWeights(input, start);
            return start;
        }
    }
}
