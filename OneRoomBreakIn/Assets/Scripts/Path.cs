public class Path
{
    public int g;         // Steps from A to this
    public int h;         // Steps from this to B
    public Path parent;   // Parent node in the path
    public int x;         // x coordinate
    public int y;         // y coordinate
    public Path(int _g, int _h, Path _parent, int _x, int _y)
    {
        g = _g;
        h = _h;
        parent = _parent;
        x = _x;
        y = _y;
    }
    public int f // Total score for this
    {
        get
        {
            return g + h;
        }
    }
}
