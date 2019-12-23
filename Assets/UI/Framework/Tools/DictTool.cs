using System.Collections.Generic;

public static class DictTool
{
    public static Tvalue GetValue<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
    {
        Tvalue value = default(Tvalue);
        dict.TryGetValue(key, out value);
        return value;
    }
}
