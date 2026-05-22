namespace LinqTasks;

public class Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X, Y;

    public override bool Equals(object? obj)
    {
        if (obj is Point other)
        {
            return X == other.X && Y == other.Y;
        }
        return base.Equals(obj);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() => $"({X}, {Y})";
}
