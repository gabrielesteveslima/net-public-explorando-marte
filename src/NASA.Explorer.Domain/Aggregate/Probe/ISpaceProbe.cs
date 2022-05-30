using System.Collections.Generic;
using NASA.Explorer.Domain.Aggregate.Mesh;

namespace NASA.Explorer.Domain.Aggregate.Probe
{
    public interface ISpaceProbe
    {
        Point GetBowOfProbe();
        Dictionary<Orientation, Point> GetOrientationPoints();
        bool TryMove(LandMesh landMesh);
        void Rotate(Direction direction);
    }
}