using System;
using System.Collections;
namespace RAD
{
    public class HashTable
    {
        private Tuple<ulong, int>[] hashtable;
        private int l;
        private int size;
        private ulong mask; //Bitmask
        private Func<ulong, ulong> hashFunction;

        public HashTable(int l, Func<ulong, ulong> hashFunction)
        {
            hashtable = new Tuple<ulong, int>[0];
            this.l = l;
            this.size = 1 << l; //2^l
            this.mask = (size << l) - 1; //Bitmask size

            hashtable = new List<Tuple<ulong, int>>[size];
            //Initialize the hashtable with empty tuples
            for (int i = 0; i < size; i++)
            {
                hashtable[i] = new List<Tuple<ulong, int>>();
            }        
        }
        
        //Returns hashed value of key
        private int Hash(ulong key)
        {
            return (int)(hashFunction(key) & mask);
        }

        //Get method retrieves the value associated with the given key (x) from the hash table.
        public int Get(ulong x)
        {
            int index = Hash(x);
            foreach (var value in table[index])
            {
                if (value.Key == x)
                    return value.Value;
            }
            return 0;
        }

        //Set method adds or updates a key-value pair in the hash table.
        public void Set(ulong x, int v)
        {
            int index = Hash(x);
            for (int i = 0; i < table[index].Count; i++)
            {
                if (table[index][i].Key == x)
                {
                    table[index][i] = new KeyValuePair<ulong, int>(x, v);
                    return;
                }
            }
            table[index].Add(new KeyValuePair<ulong, int>(x, v));
        }

        //Increment method increments the value associated with the given key (x) by the specified amount (d).
        public void Increment(ulong x, int d)
        {
            int index = Hash(x);
            for (int i = 0; i < table[index].Count; i++)
            {
                if (table[index][i].Key == x)
                {
                    int newVal = table[index][i].Value + d;
                    table[index][i] = new KeyValuePair<ulong, int>(x, newVal);
                    return;
                }
            }
            table[index].Add(new KeyValuePair<ulong, int>(x, d));
        }
        
        //Load data in to hashtable. True for increment, false for accumulation.
        public static void LoadData(HashTableChaining table, List<Tuple<ulong, int>> data, bool increment = false)
        {
            foreach (var entry in data)
            { 
                if (increment)
                    table.Increment(entry.Item1, entry.Item2);
                else
                    table.Set(entry.Item1, entry.Item2);
            }
        }
    }
}