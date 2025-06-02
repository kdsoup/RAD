using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using RAD;

namespace RAD
{
    public class Task8Runner
    {
        public static void RunTask8()
        {
            int numKeys = 23;
            int numItems = 1000;
            int numExperiments = 100;
            int[] mValues = { 128, 256, 512 };

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

            // Measure chaining time
            var stopwatch = Stopwatch.StartNew();
            BigInteger chainingS = 0;
            foreach (var bucket in hashTable.GetType().GetField("hashtable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(hashTable) as List<Tuple<ulong, int>>[])
            {
                foreach (var entry in bucket)
                {
                    chainingS += BigInteger.Pow(entry.Item2, 2);
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Chaining runtime: {stopwatch.ElapsedMilliseconds} ms");

            // For each m value
            foreach (var m in mValues)
            {
                Console.WriteLine($"\n=== Results for m = {m} ===");

                List<BigInteger> estimates = new List<BigInteger>();
                Random rnd = new Random();

                stopwatch.Restart();
                for (int i = 0; i < numExperiments; i++)
                {
                    BigInteger[] a = new BigInteger[4];
                    for (int j = 0; j < 4; j++)
                    {
                        byte[] bytes = new byte[12];
                        rnd.NextBytes(bytes);
                        a[j] = new BigInteger(bytes) & ((BigInteger.One << 89) - 1);
                    }

                    var countSketch = CountSketch.AdvancedCountSketch(stream, a, m);
                    BigInteger X = CountSketch.SecondMomentEstimation(countSketch);
                    estimates.Add(X);
                }
                stopwatch.Stop();

                // Compute MSE
                BigInteger mse = 0;
                foreach (var est in estimates)
                {
                    mse += BigInteger.Pow(est - exactS, 2);
                }
                mse /= numExperiments;

                Console.WriteLine($"Mean Square Error (MSE): {mse}");
                Console.WriteLine($"Count-Sketch runtime for m = {m}: {stopwatch.ElapsedMilliseconds} ms");
            }
        }
    }
}
