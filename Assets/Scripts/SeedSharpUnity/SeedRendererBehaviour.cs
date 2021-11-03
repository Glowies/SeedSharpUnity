using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeedSharp;

public class SeedRendererBehaviour : MonoBehaviour
{
    public GameObject LineSegmentPrefab;

    private PlantIterator _iterator;
    private SeedRendererUnity _renderer;

    // Start is called before the first frame update
    void Start()
    {
        var plant = new Plant();
        _iterator = new PlantIterator(plant);
        _renderer = new SeedRendererUnity(plant, LineSegmentPrefab);

        _iterator.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        _iterator.Iterate(Time.deltaTime * 1000);
        _renderer.Render();
    }
}
