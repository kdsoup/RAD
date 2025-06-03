using System.Numerics;
using RAD;

namespace RAD
{
    public static class SquareSum
    {
        public static long CalcSquareSum(List<Tuple<ulong, int>> stream, int l, Func<ulong, ulong> hashFunction)
        {
            //Initialize hash table
            HashTable hashTable = new HashTable(l, hashFunction);

            //Iterate through data and put it in hash table
            foreach (var (x, d) in stream)
            {
                hashTable.Increment(x, d);
            }
            long sum = 0;
            foreach (var (x, d)in hashTable.GetAllKeys())
            {
                sum += (long)d * d;
            }
            return sum;
        }
    }
}
