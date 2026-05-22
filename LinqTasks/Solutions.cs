namespace LinqTasks;

public static class Solutions
{
    public static T[] CycleLeft<T>(T[] array, int k)
    {
        if (array.Length == 0) return array;
        var shift = ((k % array.Length) + array.Length) % array.Length;
        return array.Skip(shift).Concat(array.Take(shift)).ToArray();
    }

    public static IEnumerable<Point> Neighborhood(IEnumerable<Point> points)
    {
        var offsets = (from dx in Enumerable.Range(-1, 3)
                       from dy in Enumerable.Range(-1, 3)
                       where dx != 0 || dy != 0
                       select (dx, dy)).ToArray();

        return points
            .SelectMany(p => offsets.Select(o => new Point(p.X + o.dx, p.Y + o.dy)))
            .Distinct();
    }

    public static IEnumerable<string> AtMostTwice(IEnumerable<string> strings)
        => strings.Where(s => s.GroupBy(c => c).All(g => g.Count() <= 2));
}
