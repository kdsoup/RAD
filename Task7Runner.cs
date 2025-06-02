using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using RAD;

namespace RAD
{
    public class Task7Runner
    {
        public static void RunTask7()
        {
            int numKeys = 23;
            int numItems = 1000;
            int numExperiments = 100;

            var stream = StreamGenerator.CreateStream(numItems, (int)Math.Log2(numKeys)).ToList();

            // Compute exact S using HashTable
            var hashTable = new HashTable(8, HashFunctions.MultiplyShift);
            HashTable.LoadData(hashTable, stream);
            BigInteger exactS = 0;
            foreach (var bucket in hashTable.GetType().GetField("hashtable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(hashTable) as List<Tuple<ulong, int>>[])
            {
                foreach (var entry in bucket)
                {
                    exactS += BigInteger.Pow(entry.Item2, 2);
                }
            }
            Console.WriteLine($"Exact S = {exactS}");

            // Run 100 Count-Sketch experiments
            List<BigInteger> estimates = new List<BigInteger>();
            Random rnd = new Random();

            int k = 256;
            for (int i = 0; i < numExperiments; i++)
            {
                BigInteger[] a = new BigInteger[4];
                for (int j = 0; j < 4; j++)
                {
                    byte[] bytes = new byte[12];
                    rnd.NextBytes(bytes);
                    a[j] = new BigInteger(bytes) & ((BigInteger.One << 89) - 1);
                }

                var countSketch = CountSketch.AdvancedCountSketch(stream, a, k);
                BigInteger X = CountSketch.SecondMomentEstimation(countSketch);
                estimates.Add(X);
            }

            // Sort and print estimates
            estimates.Sort();
            Console.WriteLine("\nSorted Estimates:");
            for (int i = 0; i < estimates.Count; i++)
            {
                Console.WriteLine($"{i + 1}, {estimates[i]}");
            }

            // Compute Mean Square Error
            BigInteger mse = 0;
            foreach (var est in estimates)
            {
                mse += BigInteger.Pow(est - exactS, 2);
            }
            mse /= numExperiments;
            Console.WriteLine($"\nMean Square Error (MSE): {mse}");

            // Compute medians of 9 groups
            Console.WriteLine("\nMedian of 9 Groups:");
            List<BigInteger> medians = new List<BigInteger>();
            for (int group = 0; group < 9; group++)
            {
                var groupEstimates = estimates.Skip(group * 11).Take(11).ToList();
                groupEstimates.Sort();
                BigInteger median = groupEstimates[5]; // middle element (11 elements)
                medians.Add(median);
            }
            medians.Sort();
            for (int i = 0; i < medians.Count; i++)
            {
                Console.WriteLine($"{i + 1}, {medians[i]}");
            }

            Console.WriteLine($"\nExact S (line reference): {exactS}");
        }
    }
}
