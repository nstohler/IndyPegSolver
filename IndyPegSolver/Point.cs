public struct Point : IComparable<Point>
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point Copy()
    {
        return new Point(X, Y);
    }

    public override bool Equals(object obj)
    {
        if (obj is Point other)
        {
            return X == other.X && Y == other.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public int CompareTo(Point other)
    {
        int result = X.CompareTo(other.X);
        if (result == 0)
        {
            result = Y.CompareTo(other.Y);
        }
        return result;
    }
}