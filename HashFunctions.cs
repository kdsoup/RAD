using System.Numerics;

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
        BigInteger p = BigInteger.Pow(2, q) - 1;
        BigInteger a = BigInteger.Parse("21852482231921622378");
        BigInteger b = BigInteger.Parse("72212331521915716217");
        int l = 18;
        BigInteger m = BigInteger.Pow(2, l);
        BigInteger y = 1;
        y = (y & p) + (y >> q);
        if (y >= p) { y -= p; }
        return (ulong) y;
    }

}