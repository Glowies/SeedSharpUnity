using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeedSharp;
using System.Threading.Tasks;
using System;

public class SeedRendererUnity : SeedRendererBase
{
    private Dictionary<Guid, LineRenderer> _lines;
    private GameObject _lineSegmentPrefab;

    public SeedRendererUnity(Plant plant, GameObject lineSegmentPrefab) : base(plant)
    {
        _lines = new Dictionary<Guid, LineRenderer>();
        _lineSegmentPrefab = lineSegmentPrefab;
    }

    public override void DrawLine(System.Numerics.Vector2 p1, System.Numerics.Vector2 p2, Guid segmentGuid)
    {
        if(!_lines.ContainsKey(segmentGuid))
        {
            var lineObject = GameObject.Instantiate(_lineSegmentPrefab);
            lineObject.name = segmentGuid.ToString();
            lineObject.TryGetComponent(out LineRenderer newRenderer);
            _lines.Add(segmentGuid, newRenderer);
        }

        var lineRenderer = _lines[segmentGuid];
        lineRenderer.SetPosition(0, new Vector3(p1.X, p1.Y, 0));
        lineRenderer.SetPosition(1, new Vector3(p2.X, p2.Y, 0));
        lineRenderer.startWidth = .01f;
        lineRenderer.endWidth = .01f;
    }

    public override Task DrawLineAsync(System.Numerics.Vector2 p1, System.Numerics.Vector2 p2, Guid segmentGuid)
    {
        throw new System.NotImplementedException();
    }
}
