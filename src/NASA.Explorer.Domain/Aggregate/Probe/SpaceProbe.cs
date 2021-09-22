using System;
using System.Collections.Generic;
using System.Linq;
using NASA.Explorer.Domain.Aggregate.Mesh;

namespace NASA.Explorer.Domain.Aggregate.Probe
{
    public class SpaceProbe : ISpaceProbe
    {
        public SpaceProbe(Point position, Orientation orientation)
        {
            Position = position;
            Orientation = orientation;
        }

        public Point Position { get; private set; }

        public Orientation Orientation { get; private set; }

        public Point BowOfProbe => GetBowOfProbe();

        public Point GetBowOfProbe()
        {
            var orientationPoints = GetOrientationPoints();
            var bowOfProbe = orientationPoints.FirstOrDefault(x => x.Key == Orientation);

            return bowOfProbe.Value;
        }

        public Dictionary<Orientation, Point> GetOrientationPoints()
        {
            return new Dictionary<Orientation, Point>
            {
                {
                    Orientation.N, new Point(Position.X, Position.Y + 1)
                },
                {
                    Orientation.E, new Point(Position.X + 1, Position.Y)
                },
                {
                    Orientation.W, new Point(Position.X - 1, Position.Y)
                },
                {
                    Orientation.S, new Point(Position.X, Position.Y - 1)
                }
            };
        }

        public bool TryMove(LandMesh landMesh)
        {
            var movimentOutLandMesh = BowOfProbe.X > landMesh.MaxValue.X
                                      || BowOfProbe.Y > landMesh.MaxValue.Y || BowOfProbe.X < landMesh.MinValue.X ||
                                      BowOfProbe.Y < landMesh.MinValue.Y;

            if (movimentOutLandMesh) return false;

            Position = BowOfProbe;

            return true;
        }

        public void Rotate(Direction direction)
        {
            var valueOfOrientation = (int) Orientation;

            var newValueOfOrientation = direction switch
            {
                Direction.L => valueOfOrientation - 1,
                Direction.R => valueOfOrientation + 1,
                _ => 0
            };

            Orientation = newValueOfOrientation switch
            {
                0 => Orientation.W,
                > 4 => Orientation.N,
                _ => (Orientation) Enum.ToObject(typeof(Orientation), newValueOfOrientation)
            };
        }
    }
}