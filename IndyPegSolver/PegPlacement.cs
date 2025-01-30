public struct PegPlacement : IComparable<PegPlacement>
{
    public Point Position { get; }
    public SlotState State { get; }

    public PegPlacement(Point position, SlotState state)
    {
        if (state != SlotState.Left && state != SlotState.Right)
        {
            throw new ArgumentException("Invalid peg state");
        }

        Position = position;
        State = state;
    }

    public PegPlacement Copy()
    {
        return new PegPlacement(Position.Copy(), State);
    }

    public override bool Equals(object? obj)
    {
        if (obj is PegPlacement other)
        {
            return Position.Equals(other.Position) && State == other.State;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Position, State);
    }

    public override string ToString()
    {
        return $"{Position.X}-{Position.Y}-{(State == SlotState.Left ? "L" : "R")}";
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

    public int CompareTo(PegPlacement other)
    {
        // Explicitly compare SlotState values: Right comes before Left
        int stateComparison = State == SlotState.Right ? -1 : (State == SlotState.Left ? 1 : 0);
        int otherStateComparison = other.State == SlotState.Right ? -1 : (other.State == SlotState.Left ? 1 : 0);
        int result = stateComparison.CompareTo(otherStateComparison);

        if (result != 0)
        {
            return result;
        }

        int xComparison = Position.X.CompareTo(other.Position.X);
        if (xComparison != 0)
        {
            return xComparison;
        }

        return Position.Y.CompareTo(other.Position.Y);
    }
}