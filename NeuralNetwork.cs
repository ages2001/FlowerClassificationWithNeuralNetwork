using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        private Neuron n1;
        private Neuron n2;
        private Neuron n3;

        private string[] dataset;

        public Neuron N1 { get => n1; set => n1 = value; }
        public Neuron N2 { get => n2; set => n2 = value; }
        public Neuron N3 { get => n3; set => n3 = value; }

        public string[] Dataset { get => dataset; set => dataset = value; }


        public NeuralNetwork(Neuron n1, Neuron n2, Neuron n3)
        {
            N1 = n1;
            N2 = n2;
            N3 = n3;
        }

        public double findAccuracyValue()
        {
            int totalNeutrons = 0;
            int trueNeutrons = 0;

            foreach (string dataLine in Dataset)
            {
                string[] data = dataLine.Split(',');

                N1.addInputsAndCalculate(data);
                N2.addInputsAndCalculate(data);
                N3.addInputsAndCalculate(data);

                Neuron NMax;
                string dataFlowerClass = data[Neuron.ARRAY_SIZE_LIMIT];

                if (N1.Output > N2.Output && N1.Output > N3.Output) NMax = N1;
                else if (N2.Output > N1.Output && N2.Output > N3.Output) NMax = N2;
                else NMax = N3;

                if (NMax.FlowerClass == dataFlowerClass) trueNeutrons++;

                totalNeutrons++;
            }

            double accuracyValue = (double)trueNeutrons * 100 / totalNeutrons;

            return accuracyValue;
        }

        public void train(double lambda, int epoch)
        {
            try
            {
                Dataset = File.ReadAllLines("iris.data");
            }
            catch (IOException e)
            {
                Console.WriteLine("File not found or file error!\n");
                return;
            }

            for (int i = 0; i < epoch; i++)
            {
                foreach (string dataLine in Dataset)
                {
                    string[] data = dataLine.Split(',');

                    N1.addInputsAndCalculate(data);
                    N2.addInputsAndCalculate(data);
                    N3.addInputsAndCalculate(data);

                    Neuron NMax;
                    string dataFlowerClass = data[Neuron.ARRAY_SIZE_LIMIT];

                    if (N1.Output > N2.Output && N1.Output > N3.Output) NMax = N1;
                    else if (N2.Output > N1.Output && N2.Output > N3.Output) NMax = N2;
                    else NMax = N3;
                    
                    if (dataFlowerClass == "Iris-setosa" && NMax.FlowerClass != dataFlowerClass)
                    {
                        NMax.decreaseWeights(lambda);
                        N1.increaseWeights(lambda);
                    }

                    else if (dataFlowerClass == "Iris-versicolor" && NMax.FlowerClass != dataFlowerClass)
                    {
                        NMax.decreaseWeights(lambda);
                        N2.increaseWeights(lambda);
                    }

                    else if (dataFlowerClass == "Iris-virginica" && NMax.FlowerClass != dataFlowerClass)
                    {
                        NMax.decreaseWeights(lambda);
                        N3.increaseWeights(lambda);
                    }
                }
            }
        }
    }
}
