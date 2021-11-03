using System;
namespace SeedSharp
{
    public class Plant
    {
        public RootSegment Root { get; set; }

        public Plant()
        {
            Root = new RootSegment()
            {
                LocalPosition = 0,
                Angle = PlantMath.PI / 2,
                Length = 1,
                StartWidth = .1f,
                EndWidth = .1f
            };
        }

        public int SegmentCount
        {
            get
            {
                if (Root is null)
                    return 0;
                return Root.SegmentCount;
            }
        }
    }
}
