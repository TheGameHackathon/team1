using System.Collections.Generic;

namespace Domain
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector left, Vector right) => new Vector(left.X + right.X, left.Y + right.Y);
    }

    public static class VectorExtensions
    {
        public static IEnumerable<Vector> DirectionDeltas = new[]
        {
            new Vector(1, 0),
            new Vector(0, 1),
            new Vector(-1, 0),
            new Vector(0, -1),
        };
    }
}