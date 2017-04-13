using System.Collections.Generic;

namespace neuroLab_6
{
    public static class Stuff
    {
        public static double rate;
        public static double[] target;
        public static double targetError;

        public static void InitConstants(double rate, double targetError, List<double> target)
        {
            Stuff.rate = rate;
            Stuff.targetError = targetError;
            Stuff.target = new double[target.Count];
            target.CopyTo(Stuff.target, 0);
        }

        public static void InitializeNet(int N, int J, int M, ref Net net)
        {
            for (int i = 0; i <= N; i++) //Init zero layer
            {
                net.layers[0].Add(new Neuron(0));
            }

            for (int i = 0; i <= J; i++) //Init hidden layer
            {
                if (i == 0) net.layers[1].Add(new Neuron(0)); //First neuron is Bias
                else //Normal neuron
                {
                    net.layers[1].Add(new Neuron(N + 1));
                    for (int index = 0; index < net.layers[0].Count; index++) //Init connections
                    {
                        net.layers[0][index].nextIndexes.Add(i); //Set zero layer's next connections
                        net.layers[1][i].prevIndexes.Add(index); //Set hidden layer's prev connections
                    }
                }
            }

            for (int i = 0; i < M; i++) //Init output layer
            {
                net.layers[2].Add(new Neuron(J + 1));
                for(int index = 0; index< net.layers[1].Count; index++) //Init connections
                {
                    net.layers[1][index].nextIndexes.Add(i); //Set hidden layer's next connections
                    net.layers[2][i].prevIndexes.Add(index); //Set output layer's prev connections
                }
            }
        }
    }
}
