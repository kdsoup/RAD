using System.Numerics;
using RAD;

public static class HashFunctions
{

    public static ulong MultiplyShift(ulong x)
    {
        // h(x) = (a*x)>>(64 − l)
        // rand bit from https://www.random.org/bytes/
        ulong a = 0b_01110111_01101000_11001101_00111001_01110111_11110001_01110001_00100111UL;
        int l = 59;
        return (a * x) >> (64 - l);
    }

    public static ulong MulitplyModShift(ulong x)
    {
        int q = 89;
        int l = 18;
        BigInteger y;
        BigInteger xt;
        BigInteger p = BigInteger.Pow(2, q) - 1;
        BigInteger a = BigInteger.Parse("21852482231921622378");
        BigInteger b = BigInteger.Parse("72212331521915716217");
        BigInteger m = BigInteger.Pow(2, l);

        xt = a * x + b;
        y = (xt & p) + (xt >> q);
        if (y >= p) { y -= p; }
        y &= m - 1;

        return (ulong) y;
    }


    public static void GetRunningTimes()
    {
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