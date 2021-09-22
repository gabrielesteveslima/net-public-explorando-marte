namespace NASA.Explorer.Domain.Aggregate.Mesh
{
    public class Point
    {
        public Point(int x, int y)
        {
            (X, Y) = (x, y);
        }

        public int X { get; }
        public int Y { get; }
    }
}