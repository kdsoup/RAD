
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
        return 0;
    }

}