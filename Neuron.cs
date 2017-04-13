using System;
using System.Collections.Generic;

namespace neuroLab_6
{
    public class Neuron
    {
        public double[] weights;
        public double[] inputs;
        public readonly int inputsCount;
        public List<int> prevIndexes;
        public List<int> nextIndexes;
        public double error;

        public Neuron(int count)
        {
            prevIndexes = new List<int>();
            nextIndexes = new List<int>();
            inputsCount = count;
            if (count == 0)
            {
                inputs = new double[1];
                inputs[0] = 1; // (Bias)
            }
            else
            {
                inputs = new double[count];
                weights = new double[count];
                for (int i = 0; i < count; i++) weights[i] = 0;
            }
        }

        public double RawOutput()
        {
            if (inputsCount == 0) return inputs[0]; //no inputs, return constant

            double net = 0.0;
            for (int i = 0; i < inputsCount; i++)
                net += weights[i] * inputs[i];
            return net;
        }

        public double ActivatedOutput()
        {
            if (inputsCount == 0) return inputs[0];
            return Activation(RawOutput());
        }

        public double Activation(double net)
        {
            return (1.0 - Math.Exp(-net)) / (1.0 + Math.Exp(-net));
        }

        public double Derivative()
        {
            return 0.5 * (1.0 - Math.Pow(ActivatedOutput(), 2));
        }
    }
}
