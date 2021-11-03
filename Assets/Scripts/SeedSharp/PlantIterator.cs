using System;

namespace SeedSharp
{
    public class PlantIterator
    {
        private const float _newSegmentLength = 0f;
        public Plant TargetPlant { get; set; }

        public PlantIterator(Plant targetPlant)
        {
            TargetPlant = targetPlant;
            Initialize();
        }

        public void Initialize()
        {
            if (TargetPlant.SegmentCount == 1)
            {
                var branch = new PlantSegment()
                {
                    LocalPosition = 1f,
                    Angle = 2 * PlantMath.PI,
                    Length = _newSegmentLength
                };

                for (int i = 0; i < 5; i++)
                {
                    if (i == 2)
                        continue;

                    var subbranch = new PlantSegment()
                    {
                        LocalPosition = 1f,
                        Angle = -PlantMath.PI / 2 + PlantMath.PI / 4 * i,
                        Length = _newSegmentLength,
                    };
                    TargetPlant.Root.AddChild(subbranch);
                }

                TargetPlant.Root.SetNext(branch);
            }
        }

        public void Iterate(float timeStep)
        {
            Iterate(timeStep, TargetPlant.Root, .1f);
        }

        public void Iterate(float timeStep, PlantSegment segment, float parentSpeed)
        {
            if (segment is null)
                return;

            float stepFactor = timeStep / 100;

            const float branchInterval = 2.7f;
            const float branchAngle = PlantMath.PI / 5;

            var angle = segment.Angle;
            float growthSpeed = parentSpeed
                * PlantMath.Pow(PlantMath.Cos(angle / 2), 2);

            var prevLength = segment.Length;
            if (segment != TargetPlant.Root)
            {
                // Scale growth depending on the number of children
                growthSpeed /= PlantMath.Log(segment.Parent.ChildrenCount, 2f);
                segment.Length += stepFactor * growthSpeed;
            }

            // Recursive calls
            Iterate(timeStep, segment.Next, growthSpeed);
            foreach(var child in segment.Children)
            {
                Iterate(timeStep, child, growthSpeed);
            }


            // Try create new branches
            var exponent = PlantMath.Ceil(PlantMath.Log2(prevLength / branchInterval));
            var pow = (int) PlantMath.Pow(2, exponent);

            if (exponent > -1 && segment.Length > pow * branchInterval)
            {
                for(float i = 0; i < pow; i++)
                {
                    var position = 1f / pow / 2f + i / pow;
                    var leftBranch = new PlantSegment()
                    {
                        Angle = -branchAngle,
                        LocalPosition = position,
                        Length = _newSegmentLength
                    };
                    var rightBranch = new PlantSegment()
                    {
                        Angle = branchAngle,
                        LocalPosition = position,
                        Length = _newSegmentLength
                    };

                    segment.AddChild(leftBranch);
                    segment.AddChild(rightBranch);
                }
            }
        }
    }
}
