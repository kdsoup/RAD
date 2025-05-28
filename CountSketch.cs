using System.Globalization;
using System.Numerics;
using RAD;

public static class CountSketch
{

    public static BigInteger FourUniversalHashFunction(BigInteger x)
    {
        int b = 89;
        BigInteger y;
        BigInteger p = BigInteger.Pow(2, b) - 1;
        BigInteger a0 = BigInteger.Parse("2081226");
        BigInteger a1 = BigInteger.Parse("1210017");
        BigInteger a2 = BigInteger.Parse("2960218");
        BigInteger a3 = BigInteger.Parse("1947137");
        BigInteger[] a = new BigInteger[] { a0, a1, a2, a3 };

        y = a[3];
        for (int i = 2; i >= 0; i--)
        {
            y = y * x + a[i];
            y = (y & p) + (y >> b);
        }
        if (y >= p) { y -= p; }

        return y;
    }




}