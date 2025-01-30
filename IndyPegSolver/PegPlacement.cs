public struct PegPlacement
{
    public Point Point { get; }
    public SlotState State { get; }

    public PegPlacement(Point point, SlotState state)
    {
        if (state != SlotState.Left && state != SlotState.Right)
        {
            throw new ArgumentException("Invalid peg state");
        }

        Point = point;
        State = state;
    }

    public PegPlacement Clone()
    {
        return new PegPlacement(Point.Clone(), State);
    }

    public override bool Equals(object? obj)
    {
        if (obj is PegPlacement other)
        {
            return Point.Equals(other.Point) && State == other.State;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Point, State);
    }

    public override string ToString()
    {
        return $"{Point.X}-{Point.Y}-{(State == SlotState.Left ? "L" : "R")}";
    }

    public static PegPlacement FromString(string placementString)
    {
        var parts = placementString.Split('-');
        if (parts.Length != 3 || !int.TryParse(parts[0], out int x) || !int.TryParse(parts[1], out int y))
        {
            throw new ArgumentException("Invalid peg placement string format");
        }
        var position = new Point(x, y);
        var state = parts[2] switch
        {
            "L" => SlotState.Left,
            "R" => SlotState.Right,
            _ => throw new ArgumentException("Invalid peg state in string")
        };
        return new PegPlacement(position, state);
    }
}