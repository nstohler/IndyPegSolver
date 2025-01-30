
public struct BoardRating
{
    public int PegCount { get; }
    public int UnfilledHolesCount { get; }

    public BoardRating(int pegCount, int unfilledHolesCount)
    {
        PegCount = pegCount;
        UnfilledHolesCount = unfilledHolesCount;
    }

    public override string ToString()
    {
        return $"[{PegCount}-{UnfilledHolesCount}]";
    }
}