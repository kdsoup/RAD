using System;
using System.Collections.Generic;

namespace RAD
{
    public class Program
    {
        // ChatGPT generet dummy eksempel der blot er indsat for at få programmet til at køre
        public static void Main(string[] args)
        {
            foreach (var tuple in StreamGenerator.CreateStream(9, 10))
            {
                Console.WriteLine(tuple);
            }

            Console.WriteLine(HashFunctions.MultiplyShift(322));
        }
    }
}
