using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeedSharp;

public class SeedRendererBehaviour : MonoBehaviour
{
    public GameObject LineSegmentPrefab;
    public GameObject LeafSegmentPrefab;

    public bool IsRunning
    {
        get;
        private set;
    }

    private PlantIterator _iterator;
    private SeedRendererUnity _renderer;

    // Start is called before the first frame update
    void Start()
    {
        var plant = new Plant();
        _iterator = new PlantIterator(plant);
        _renderer = new SeedRendererUnity(plant, LineSegmentPrefab, LeafSegmentPrefab);

        _iterator.Initialize();
        IsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsRunning = !IsRunning;
        }

        if (IsRunning)
        {
            _iterator.Iterate(Time.deltaTime * 1000);
            _renderer.Render();
        }
    }
}
