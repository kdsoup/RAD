using System.Numerics;
using RAD;

public static class Testing
{
    public static void HashtableTest()
    {
        IEnumerable<Tuple<ulong, int>> stream = StreamGenerator.CreateStream(20, 1);
            Hashtable table2 = new Hashtable(23, HashFunctions.MulitplyModShift);
            foreach ((var x, var v) in stream)
            {
                table2.Increment(x, v);
                Console.WriteLine("key: (" + x + ", " + v + ")");
            }
            var index = 3;
            Console.WriteLine(HashFunctions.MulitplyModShift(stream.ElementAt(index).Item1, 23));
            Console.WriteLine(table2.Get(stream.ElementAt(index).Item1));
    }
}