using System;
using System.Collections.Generic;

namespace neuroLab_6
{
    class Program
    {
        static void Main(string[] args)
        {
            Net net = new Net(3); //Net with 3 layers

            /*
            Stuff.InitializeNet(3, 3, 4, ref net); //N=3, J=3, M=4
            Stuff.InitConstants(1.0, Math.Pow(10, -3), new List<double> { 0.1, -0.6, 0.2, 0.7 }); //Set learning rate, target error, target output
            net.SetInputs(new List<double> { 1, 0.3, -0.1, 0.9 }); 
            */

            Stuff.InitializeNet(2, 1, 2, ref net); //N=2, J=1, M=2
            Stuff.InitConstants(0.5, Math.Pow(10, -3), new List<double> { 0.2, -0.1 }); //Set learning rate, target error, target output
            net.SetInputs(new List<double> { 1, 1, -1 });
            
            net.StartLearning();

        }
    }
}
