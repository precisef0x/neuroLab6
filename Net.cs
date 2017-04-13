using System;
using System.Collections.Generic;

namespace neuroLab_6
{
    public class Net
    {
        public List<List<Neuron>> layers;
        public List<double> outputs;

        public Net(int layersCount)
        {
            outputs = new List<double>();
            layers = new List<List<Neuron>>();
            for (int i = 0; i < layersCount; i++)
                layers.Add(new List<Neuron>());
        }

        public void StartLearning()
        {
            int counter = 0;
            do
            {
                RunEpoch();
                Console.WriteLine("Epoch {0} RMSE = {1}", counter, GetRMSE());
                counter++;
            }
            while (GetRMSE() > Stuff.targetError);
        }

        public void RunEpoch()
        {
            ProcessConnections();
            CalculateErrors();
            UpdateWeights();
        }

        public void UpdateWeights()
        {
            foreach (List<Neuron> layer in layers)
            {
                foreach (Neuron neuron in layer)
                {
                    if (neuron.inputsCount != 0)
                    {
                        for (int i = 0; i < neuron.weights.Length; i++)
                        {
                            neuron.weights[i] += Stuff.rate * neuron.inputs[i] * neuron.error; //Update weight
                        }
                    }
                }
            }
        }

        public void CalculateErrors()
        {
            for (int i = 0; i < outputs.Count; i++) //Update output layer's neurons' errors
            {
                layers[layers.Count - 1][i].error = layers[layers.Count - 1][i].Derivative() * (Stuff.target[i] - outputs[i]);
            }
            for (int lI = layers.Count - 2; lI > 0; lI--) //Update the rest; lI - current layer index
            {
                for(int nI=0; nI < layers[lI].Count; nI++) //nI - current neuron index
                {
                    if (layers[lI][nI].inputsCount == 0) break; //Doesn't need error calculation
                    double sum = 0.0;
                    for (int i = 0; i < layers[lI][nI].nextIndexes.Count; i++) //Calc sum; i - connection number
                    {
                        sum += layers[lI + 1][layers[lI][nI].nextIndexes[i]].error * layers[lI + 1][layers[lI][nI].nextIndexes[i]].weights[nI];
                    }
                    layers[lI][nI].error = layers[lI][nI].Derivative() * sum; //Update error
                }
            }
        }

        public double GetRMSE()
        {
            double sum = 0.0;
            for (int i = 0; i < outputs.Count; i++)
                sum += Math.Pow((Stuff.target[i] - outputs[i]), 2);
            return Math.Sqrt(sum);
        }

        public void SetInputs(List<double> values)
        {
            for (int i = 0; i < layers[0].Count; i++)
            {
                layers[0][i].inputs[0] = values[i];
            }
        }

        public void ProcessConnections()
        {
            for (int lI = 1; lI < layers.Count; lI++) //calculate all inputs, lI - current layer index
            {
                foreach (Neuron neuron in layers[lI])
                {
                    for (int i = 0; i < neuron.inputsCount; i++)
                    {
                        neuron.inputs[i] = layers[lI - 1][neuron.prevIndexes[i]].ActivatedOutput();
                    }
                }
            }
            for (int i = 0; i < layers[layers.Count - 1].Count; i++) //calculate output value for each neuron in the output layer
            {
                if (i + 1 > outputs.Count) outputs.Add(0.0);
                outputs[i] = layers[layers.Count - 1][i].ActivatedOutput();
            }
        }
    }
}
