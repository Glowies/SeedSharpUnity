using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeedSharp;
using System.Threading.Tasks;

public class SeedRendererUnity : SeedRendererBase
{
    public SeedRendererUnity(Plant plant) : base(plant)
    {

    }

    public override void DrawLine(System.Numerics.Vector2 p1, System.Numerics.Vector2 p2)
    {
        throw new System.NotImplementedException();
    }

    public override Task DrawLineAsync(System.Numerics.Vector2 p1, System.Numerics.Vector2 p2)
    {
        throw new System.NotImplementedException();
    }
}
