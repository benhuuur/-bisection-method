namespace MazeAI;

public class Space // this is a node??
{
    public int X { get; init; }
    public int Y { get; init; }
    public Space? Top { get; set; }
    public Space? Left { get; set; }
    public Space? Right { get; set; }
    public Space? Bottom { get; set; }
    public bool Visited { get; set; } = false; //make the blue mark
    public bool IsSolution { get; set; } = false; //make the red line
    public bool Exit { get; set; } = false;

    public Space[] Neighbours() =>
        new Space[] { this.Left!, this.Right!, this.Top!, this.Bottom! };

    public void Reset()
    {
        IsSolution = false;
        Visited = false;
    }
}
