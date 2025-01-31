using System.Collections.Generic;

public class PointComparer : IComparer<Point>
{
    public int Compare(Point x, Point y)
    {
        int result = x.X.CompareTo(y.X);
        if (result == 0)
        {
            result = x.Y.CompareTo(y.Y);
        }
        return result;
    }
}