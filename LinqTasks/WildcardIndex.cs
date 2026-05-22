namespace LinqTasks;

public class WildcardIndex
{
    private readonly Dictionary<string, List<string>> index = new();
    private readonly int k;

    public WildcardIndex(IEnumerable<string> words)
    {
        var list = words.ToList();
        if (list.Count == 0) return;
        k = list[0].Length;

        foreach (var word in list)
        {
            for (int i = 0; i < k; i++)
            {
                var key = word.Remove(i, 1).Insert(i, "*");
                if (!index.TryGetValue(key, out var bucket))
                    index[key] = bucket = new List<string>();
                bucket.Add(word);
            }
        }
    }

    public IEnumerable<string> NeighborsOf(string word)
    {
        var seen = new HashSet<string>();
        for (int i = 0; i < k; i++)
        {
            var key = word.Remove(i, 1).Insert(i, "*");
            if (index.TryGetValue(key, out var bucket))
            {
                foreach (var w in bucket)
                    if (w != word && seen.Add(w))
                        yield return w;
            }
        }
    }
}
