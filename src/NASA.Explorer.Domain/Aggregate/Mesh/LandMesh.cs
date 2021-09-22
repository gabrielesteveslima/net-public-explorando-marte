namespace NASA.Explorer.Domain.Aggregate.Mesh
{
    public class LandMesh
    {
        public LandMesh(int width, int height)
        {
            MinValue = new Point(0, 0);
            MaxValue = new Point(width, height);
        }

        public Point MinValue { get; }
        public Point MaxValue { get; }
    }
}