using System;

namespace NeuralNetwork
{
    public class Neuron
    {
        public static int ARRAY_SIZE_LIMIT = 4;

        private double[] lengths;
        private double[] weights;
        private string flowerClass;
        private double output;

        public double[] Lengths { get => lengths; set => lengths = value; }
        public double[] Weights { get => weights; set => weights = value; }
        public string FlowerClass { get => flowerClass; set => flowerClass = value; }
        public double Output { get => output; set => output = value; }


        public Neuron(string flowerClass)
        {
            Lengths = new double[ARRAY_SIZE_LIMIT];
            Weights = new double[ARRAY_SIZE_LIMIT];
            FlowerClass = flowerClass;

            Random random = new Random();
            for (int i = 0; i < ARRAY_SIZE_LIMIT; i++)
                Weights[i] = random.NextDouble();
        }

        public void addInputsAndCalculate(string[] data)
        {
            Output = 0.0;
            for (int i = 0; i < ARRAY_SIZE_LIMIT; i++)
            {
                Lengths[i] = Convert.ToDouble(data[i]) / 10.0;
                output += Weights[i] * Lengths[i];
            }
        }

        public void decreaseWeights(double lambda)
        {
            for (int i = 0; i < ARRAY_SIZE_LIMIT; i++)
                Weights[i] -= lambda * Lengths[i];
        }

        public void increaseWeights(double lambda)
        {
            for (int i = 0; i < ARRAY_SIZE_LIMIT; i++)
                Weights[i] += lambda * Lengths[i];
        }
    }
}
