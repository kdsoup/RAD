using System.Numerics;
using RAD;

public class Hashtable
{

    private List<(ulong, int)>[] table { get; set; }
    private Func<ulong, int, ulong> h { get; set; }
    public int l { get; set; }

    public Hashtable(int l, Func<ulong, int, ulong> hashfunction)
    {
        table = new List<(ulong, int)>[(int)Math.Pow(2, l)];
        h = hashfunction;
        this.l = l;
    }

    public int Get(ulong x)
    {
        ulong hx = h(x, this.l);
        if (table[hx] == null) { return 0; }
        else if (!table[hx].Exists(t => t.Item1 == x)) { return 0; }
        else { return table[hx].Find(t => t.Item1 == x).Item2; }
    }

    public void Set(ulong x, int v)
    {
        ulong hx = h(x, this.l);
        if (table[hx] == null) { table[hx] = new List<(ulong, int)>(); }
        
        int index = table[hx].FindIndex(t => t.Item1 == x);
        if (index >= 0) { table[hx][index] = (x, v); }
        else { table[hx].Add((x, v)); }
    }

    public void Increment(ulong x, int d)
    {
        ulong hx = h(x, this.l);
        int incrementedValue = d + Get(x);
        Set(x, incrementedValue);
    }
    
}