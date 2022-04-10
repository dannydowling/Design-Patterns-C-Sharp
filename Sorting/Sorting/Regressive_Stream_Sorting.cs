using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Sorting
{
    public enum Actions
    {
        Num_Actions
    }

    public static class RecursionModel
    {
        public static Random random = new Random();
        public static int Num_Actions = Convert.ToInt32(Actions.Num_Actions);
        public static double[] regretSum = new double[Num_Actions];
        public static double[] strategy = new double[Num_Actions];
        public static double[] strategySum = new double[Num_Actions];

        public static string[] _weightArgs = new string[int.MaxValue];
        public static double[] weights = new double[_weightArgs.Length];
        public static string[] _dimensionArgs = new string[int.MaxValue];
        public static double[] dimensions = new double[_dimensionArgs.Length];
        public static Stream[] streams;

        public static IEnumerable<Stream>[] RecursiveModel(this double[] weightArgs, double[] dimensionArgs, Stream[] dataInput)
        {
            Console.WriteLine("Regression Trained Probability");

            weights = weightArgs;
            dimensions = dimensionArgs;
            streams = dataInput;

            if (weightArgs == null)
            {
                Console.WriteLine("Enter the bias weights");
                //comma seaparate the values
                _weightArgs = (Console.ReadLine().Split('\u002C'));

                //add them to the array of weights
                foreach (var item in _weightArgs)
                { weights.Append(int.Parse(item)); }
            }

            if (dimensionArgs == null)
            {
                Console.WriteLine("Enter the list of dimensions that the data set can operate on");
                _dimensionArgs = Console.ReadLine().Split('\u002c');
                for (int i = 0; i < _dimensionArgs.Length; i++)
                {
                    //add the name of the dimension at its index to the string array.
                    dimensions.Append(i + int.Parse(_dimensionArgs[i]));
                }
            }

            try
            {
                //make the array of streams enumerable
                IEnumerable<Stream> streamArray = streams.ToEnumerableArray();
                //we'd need an array of streams to rebuild the input as enumerable into
                IEnumerable<Stream>[] arrayOfStreams = new IEnumerable<Stream>[dataInput.Count()];
                //we're going through the streams using an iterator and using clone to create a copy of the array.
                foreach (var item in streams)
                {
                    arrayOfStreams.Clone();
                }
                IEnumerable<Stream>[] result = TrainThenRun(arrayOfStreams);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static IEnumerable<Stream> ToEnumerableArray(this Stream[] source)
        {
            //we'll iterate over the number of streams
            for (int i = 0; i < source.Count(); i++)
            {
                //returning for each one, its own data structure
                yield return source[i];
            }
        }


        public static IEnumerable<Stream>[] TrainThenRun(IEnumerable<Stream>[] input)
        {
            train(1000000);
            var result = GetAverageStrategy(input);
            return result;
        }

        private static double[] getStrategy()
        {
            double normalizingSum = 0;
            for (int i = 0; i < Num_Actions; i++)
            {
                strategy[i] = regretSum[i] > 0 ? regretSum[i] : 0;
                normalizingSum += strategy[i];
            }
            for (int i = 0; i < Num_Actions; i++)
            {
                if (normalizingSum > 0)
                {
                    strategy[i] /= normalizingSum;
                }
                else
                {
                    strategy[i] = 1.0 / Num_Actions;
                    strategySum[i] += strategy[i];
                }
            }
            return strategy;
        }
        private static IEnumerable<Stream>[] GetAverageStrategy(IEnumerable<Stream>[] input)
        {
            double[] avgStrategy = new double[Num_Actions];
            double normalizingSum = 0;
            for (int i = 0; i < Num_Actions; i++)
            {
                normalizingSum += strategySum[i];
            }
            for (int i = 0; i < Num_Actions; i++)
            {
                if (normalizingSum > 0)
                {
                    avgStrategy[i] = strategySum[i] / normalizingSum;
                }
                else
                {
                    avgStrategy[i] = 1.0 / Num_Actions;
                }
            }
            input.OrderBy(x => avgStrategy[Convert.ToInt32(x)]);
            return input;
        }

        private static void train(int iterations)
        {
            double[] actionUtility = new double[Num_Actions];
            for (int i = 0; i < iterations; i++)
            {
                double[] strategy = getStrategy();
                int myAction = getAction(strategy);
                int otherAction = getAction(weights);
                actionUtility[otherAction] = 0;
                actionUtility[otherAction == Num_Actions - 1 ? 0 : otherAction + 1] = 1;
                actionUtility[otherAction == 0 ? Num_Actions - 1 : otherAction - 1] = -1;
                for (int j = 0; j < Num_Actions; j++)
                {
                    regretSum[j] += actionUtility[j] - actionUtility[myAction];
                }
            }
        }

        private static int getAction(double[] strategy)
        {
            double r = random.NextDouble();
            int a = 0;
            double cumulativeProbability = 0;
            while (a < Num_Actions - 1)
            {
                cumulativeProbability += strategy[a];
                if (r < cumulativeProbability)
                {
                    break;
                }
                a++;
            }
            return a;
        }
    }
}

