using System;
using System.Collections.Generic;
using System.Numerics;
using System.Transactions;
using System.Diagnostics;
using System.Linq;

namespace RAD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int n = 300000;
            int lmin = 10;
            int lmax = 18;

            for (int l = lmin; l <= lmax; l += 2 )
            {
            var stream = StreamGenerator.CreateStream(n, l).ToList();

            foreach (var (name, hashFunc) in new[]
            {
                    ("MultiplyShift", (Func<ulong, ulong>)HashFunctions.MultiplyShift),
                    ("MultiplyMod",   (Func<ulong, ulong>)HashFunctions.MultiplyModShift)
                })
                {
                    try
                    {
                        var sw = Stopwatch.StartNew();
                        long sum = SquareSum.CalcSquareSum(stream, l, hashFunc);
                        sw.Stop();

                        Console.WriteLine($"{l}\t{name}\t\t{sw.ElapsedMilliseconds}\t\t{sum}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{l}\t{name}\t\tFAILED\t\t{ex.Message}");
                        break;
                    }
                }
            }
            
          /*  
            // Use either MultiplyShift or MultiplyMod
            var resultShift = SquareSum.CalcSquareSum(stream, l, HashFunctions.MultiplyShift);
            Console.WriteLine($"Kvadratsum med MultiplyShift (l={l}): {resultShift}");

            // You must re-generate the stream since IEnumerable is consumed
            stream = StreamGenerator.CreateStream(n, l);
            var resultMod = SquareSum.CalcSquareSum(stream, l, HashFunctions.MulitplyModShift);
            Console.WriteLine($"Kvadratsum med MultiplyMod (l={l}):    {resultMod}");
*/
            // foreach (var tuple in StreamGenerator.CreateStream(9, 10))
            // {
            //     Console.WriteLine(tuple);
            //     Console.WriteLine(HashFunctions.MultiplyShift(tuple.Item1));
            // }

            // Opgave 1.c
            //HashFunctions.GetRunningTimes();

            //Hash table test
            /*
            HashTable hashTabletable = new HashTable(4, HashFunctions.MultiplyShift); //2^4 = 16 buckets

            Console.WriteLine("Test 1: Set and Get");
            hashTabletable.Set(42UL, 10);
            Console.WriteLine(hashTabletable.Get(42UL) == 10 ? "PASS" : "FAIL");

            Console.WriteLine("Test 2: Overwrite");
            hashTabletable.Set(42UL, 99);
            Console.WriteLine(hashTabletable.Get(42UL) == 99 ? "PASS" : "FAIL");

            Console.WriteLine("Test 3: Increment existing");
            hashTabletable.Increment(42UL, 1);
            Console.WriteLine(hashTabletable.Get(42UL) == 100 ? "PASS" : "FAIL");

            Console.WriteLine("Test 4: Increment new key");
            hashTabletable.Increment(999UL, 7);
            Console.WriteLine(hashTabletable.Get(999UL) == 7 ? "PASS" : "FAIL");

            Console.WriteLine("Test 5: LoadData");
            var testData = new List<Tuple<ulong, int>>()
            {
                Tuple.Create(1UL, 35),
                Tuple.Create(2UL, 25),
            };
            HashTable.LoadData(hashTabletable, testData);
            Console.WriteLine(hashTabletable.Get(1UL) == 35 ? "PASS" : "FAIL");
            Console.WriteLine(hashTabletable.Get(2UL) == 25 ? "PASS" : "FAIL");
            */
/*
            // Count Sketch
            IEnumerable<Tuple<ulong, int>> stream = StreamGenerator.CreateStream(100, 23);
            BigInteger[] countSketch = CountSketch.BasicCountSketch(stream);
            BigInteger secondMoment = CountSketch.SecondMomentEstimation(countSketch);
            Console.WriteLine("Second Moment Estimate: " + secondMoment);

            // Console.WriteLine("Test 7: Experiment with implementation");
            Task7Runner.RunTask7();

            // Console.WriteLine("Test 8: Experiment with implementation");
            Task8Runner.RunTask8();
            */
        }
    }
}
