using System.Numerics;

namespace SeedSharp
{
    public class RootSegment: PlantSegment
    {
        public override Matrix3x2 LocalToWorldMatrix
        {
            get => Matrix3x2.Identity;
        }

        public RootSegment()
        {
        }
    }
}
