using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeedSharp;
using System.Threading.Tasks;
using System;

public class SeedRendererUnity : SeedRendererBase
{
    private readonly Dictionary<Guid, LineRenderer> _lines;
    private readonly Dictionary<Guid, LineRenderer> _leaves;
    private readonly GameObject _lineSegmentPrefab;
    private readonly GameObject _leafSegmentPrefab;

    public SeedRendererUnity(Plant plant, GameObject lineSegmentPrefab, GameObject leafSegmentPrefab) : base(plant)
    {
        _lines = new Dictionary<Guid, LineRenderer>();
        _leaves = new Dictionary<Guid, LineRenderer>();
        _lineSegmentPrefab = lineSegmentPrefab;
        _leafSegmentPrefab = leafSegmentPrefab;
    }

    public override void DrawLine(System.Numerics.Vector2 p1, System.Numerics.Vector2 p2, Guid segmentGuid)
    {
        if(!_lines.ContainsKey(segmentGuid))
        {
            var lineObject = GameObject.Instantiate(_lineSegmentPrefab);
            lineObject.name = segmentGuid.ToString();
            lineObject.TryGetComponent(out LineRenderer newRenderer);
            _lines.Add(segmentGuid, newRenderer);

            var leafObject = GameObject.Instantiate(_leafSegmentPrefab, lineObject.transform);
            leafObject.name = "leaf";
            leafObject.TryGetComponent(out LineRenderer newLeafRenderer);
            _leaves.Add(segmentGuid, newLeafRenderer);
        }

        var lineRenderer = _lines[segmentGuid];
        lineRenderer.SetPosition(0, new Vector3(p1.X, p1.Y, 0));
        lineRenderer.SetPosition(1, new Vector3(p2.X, p2.Y, 0));
        lineRenderer.startWidth = .08f;
        lineRenderer.endWidth = .08f;

        var leafRenderer = _leaves[segmentGuid];
        var pos0 = new Vector3(p1.X, p1.Y, .01f);
        var pos1 = new Vector3(p2.X, p2.Y, .01f);
        leafRenderer.SetPosition(0, Vector3.Lerp(pos0, pos1, -.1f));
        leafRenderer.SetPosition(1, Vector3.Lerp(pos0, pos1, .25f));
        leafRenderer.SetPosition(2, Vector3.Lerp(pos0, pos1, .75f));
        leafRenderer.SetPosition(3, Vector3.Lerp(pos0, pos1, 1.10f));

        // Set the width curve of the leaf
        var keyframes = new Keyframe[] {
            new Keyframe(0, .08f),
            new Keyframe(.25f, Vector3.Distance(pos0, pos1) * .30f),
            new Keyframe(.75f, Vector3.Distance(pos0, pos1) * .35f),
            new Keyframe(1, .08f)
        };
        leafRenderer.widthCurve = new AnimationCurve(keyframes);
        leafRenderer.widthMultiplier = 1;
    }

    public override Task DrawLineAsync(System.Numerics.Vector2 p1, System.Numerics.Vector2 p2, Guid segmentGuid)
    {
        throw new System.NotImplementedException();
    }
}
