namespace NeuralNetwork
{
    public class Program
    {
        public static void printMatrix(int[] epochValues, double[] lambdaValues, double[,] accuracyValues)
        {
            Console.WriteLine("\n                 Accuracy Values\n                *****************\n");

            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("| E \\ L |    {0}    |    {1}     |    {2}    |", lambdaValues[0], lambdaValues[1], lambdaValues[2]);
            Console.WriteLine("---------------------------------------------------");

            for (int i = 0; i < epochValues.Length; i++)
            {
                Console.Write("|");

                if (epochValues[i] < 100) Console.Write("  ");
                else Console.Write(" ");

                Console.Write(epochValues[i].ToString() + "   |   ");

                for (int j = 0; j < lambdaValues.Length; j++)
                {
                    Console.Write(accuracyValues[i, j].ToString("0.000") + " %  |   ");
                }
                Console.WriteLine("\n---------------------------------------------------");
            }
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("File name is missing!\nExample: FlowerClassification.exe iris.data");
                Environment.Exit(1);
            }

            int[] epochValues = { 20, 50, 100 };
            double[] lambdaValues = { 0.005, 0.01, 0.025 };

            double[,] accuracyValues = new double[epochValues.Length, lambdaValues.Length];

            for (int count = 0; count < 3; count++)
            {
                for (int i = 0; i < epochValues.Length; i++)
                {
                    for (int j = 0; j < lambdaValues.Length; j++)
                    {
                        Neuron N1 = new Neuron("Iris-setosa");
                        Neuron N2 = new Neuron("Iris-versicolor");
                        Neuron N3 = new Neuron("Iris-virginica");
                        NeuralNetwork network = new NeuralNetwork(N1, N2, N3);

                        network.train(args[0], lambdaValues[j], epochValues[i]);
                        accuracyValues[i, j] = network.findAccuracyValue();
                    }
                }

                printMatrix(epochValues, lambdaValues, accuracyValues);
            }
        }
    }
}
