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
            IEnumerable<Tuple<ulong, int>> stream = StreamGenerator.CreateStream(9000, 10000);

            DateTime start;
            DateTime end;
            TimeSpan ts;

            start = DateTime.Now;
            var sumA = stream.Aggregate(new BigInteger(0), (accumulator, current) =>
                accumulator + HashFunctions.MultiplyShift(current.Item1)
            );
            end = DateTime.Now;
            ts = (end - start);
            Console.WriteLine($"Multiply-shift:\n Sum: {sumA}\n Running-time: {ts.TotalMilliseconds} ms");

            start = DateTime.Now;
            var sumB = stream.Aggregate(new BigInteger(0), (accumulator, current) =>
                accumulator + HashFunctions.MulitplyModShift(current.Item1)
            );
            end = DateTime.Now;
            ts = (end - start);
            Console.WriteLine($"Multiply-mod-shift:\n Sum: {sumB}\n Running-time: {ts.Milliseconds} ms");

        }
    }
}
