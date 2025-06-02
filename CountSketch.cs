using System.Numerics;
using RAD;

public static class CountSketch
{
    private const int t = 10;
    private const double epsilon = 0.01;

    public static BigInteger FourUniversalHashFunction(BigInteger x)
    {
        int b = 89;
        BigInteger y; 
        BigInteger p = BigInteger.Pow(2, b) - 1;
        int a0 = 2081226;
        int a1 = 1210017;
        int a2 = 2960218;
        int a3 = 1947137;
        BigInteger[] a = new BigInteger[] { a0, a1, a2, a3 };

        y = a3;
        for (int i = 2; i >= 0; i--)
        {
            y = y * x + a[i];
            y = (y & p) + (y >> b);
        }
        if (y >= p) { y -= p; }
        return y;
    }


    public static Tuple<BigInteger, BigInteger> HashFunction(int t, BigInteger x)
    {
        if (t > 64)
        {
            throw new ArgumentOutOfRangeException(nameof(t), "t must be less than or equal to 64.");
        }

        int b = 89;
        ulong m = (ulong)Math.Pow(2, t);
        BigInteger gx = FourUniversalHashFunction(x);
        BigInteger hx = gx & (m - 1);       //Least signigicant bits
        BigInteger bx = gx >> (b - 1);      //Most significant bits
        BigInteger sx = 1 - 2 * bx;

        return Tuple.Create(hx, sx);
    }

    public static BigInteger[] BasicCountSketch(IEnumerable<Tuple<ulong, int>> stream)
    {
        int n = stream.Count();
        int k = (int)Math.Ceiling(4 / Math.Pow(epsilon, 2));
        BigInteger[] C = new BigInteger[k];
        for (int j = 0; j < n; j++)
        {
            (ulong x, int delta) = stream.ElementAt(j);
            (var hx, var sx) = HashFunction(t, x);  // pick hash functions
            C[(int)hx] += delta * sx;
        }
        return C;
    }

    public static BigInteger Query(ulong x, BigInteger[] CountSketch)
    {
        (var hx, var sx) = HashFunction(t, x);
        return sx * CountSketch[(int)hx];
    }

    public static BigInteger SecondMomentEstimation(BigInteger[] countSketch)
    {
        
        return countSketch.Aggregate(BigInteger.Parse("0"), (acc, x) => acc + BigInteger.Pow(x,2));
    }

    public static void TestBasicCountSketch()
    {
        var stream = StreamGenerator.CreateStream(10, 1);
        foreach (var tuple in stream)
        {
            Console.WriteLine(tuple);
            Console.WriteLine(CountSketch.HashFunction(10, tuple.Item1));
        }
        var hash = CountSketch.FourUniversalHashFunction(234234876886982634);
        Console.WriteLine(hash);
        Console.WriteLine(hash & (89 - 1));
        var countSketch = CountSketch.BasicCountSketch(stream);
        Console.WriteLine(CountSketch.Query(stream.ElementAt(1).Item1, countSketch));
    }

}