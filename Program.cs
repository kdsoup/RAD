using System;
using System.Collections.Generic;
using System.Numerics;
using System.Transactions;

namespace RAD
{
    public class Program
    {
        // ChatGPT generet dummy eksempel der blot er indsat for at få programmet til at køre
        public static void Main(string[] args)
        {
            // foreach (var tuple in StreamGenerator.CreateStream(9, 10))
            // {
            //     Console.WriteLine(tuple);
            //     Console.WriteLine(HashFunctions.MultiplyShift(tuple.Item1));
            // }

            // Opgave 1.c
            //HashFunctions.GetRunningTimes();

            // Opgave 3
            // CountSketch.TestBasicCountSketch();

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
        }
    }
}
