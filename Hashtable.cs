using System;
using System.Collections;
namespace RAD
{
    public class HashTable
    {
        private List<Tuple<ulong, int>>[] hashtable;
        private int size;
        private ulong mask; //Bitmask
        private Func<ulong, ulong> hashFunction;

        public HashTable(int l, Func<ulong, ulong> hashFunction)
        {
            hashtable = new List<Tuple<ulong, int>>[0];
            this.size = 1 << l; //2^l
            this.mask = (ulong)(size - 1); //Bitmask size
            this.hashFunction = hashFunction;

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
            foreach (var value in hashtable[index])
            {
                if (value.Item1 == x)
                    return value.Item2;
            }
            return 0;
        }

        //Set method adds or updates a key-value pair in the hash table.
        public void Set(ulong x, int v)
        {
            int index = Hash(x);
            for (int i = 0; i < hashtable[index].Count; i++)
            {
                if (hashtable[index][i].Item1 == x)
                {
                    hashtable[index][i] = new Tuple<ulong, int>(x, v);
                    return;
                }
            }
            hashtable[index].Add(new Tuple<ulong, int>(x, v));
        }

        //Increment method increments the value associated with the given key (x) by the specified amount (d).
        public void Increment(ulong x, int d)
        {
            int index = Hash(x);
            for (int i = 0; i < hashtable[index].Count; i++)
            {
                if (hashtable[index][i].Item1 == x)
                {
                    int newVal = hashtable[index][i].Item2 + d;
                    hashtable[index][i] = new Tuple<ulong, int>(x, newVal);
                    return;
                }
            }
            hashtable[index].Add(new Tuple<ulong, int>(x, d));
        }
        
        //Load data in to hashtable. True for increment, false for replacement.
        public static void LoadData(HashTable table, IEnumerable<Tuple<ulong, int>> data)
        {
            foreach (var entry in data)
            { 
                    table.Increment(entry.Item1, entry.Item2);
            }
        }

        public IEnumerable<(ulong key, int value)> GetAllKeys()
        {
            foreach (var bucket in hashtable)
            {
                foreach (var keyvalue in bucket)
                {
                    yield return (keyvalue.Item1, keyvalue.Item2);
                }
            }
        }



    }
}