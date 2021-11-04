using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeedSharp;

public class SeedRendererBehaviour : MonoBehaviour
{
    public GameObject LineSegmentPrefab;
    public GameObject LeafSegmentPrefab;
    public float StopTime = 10;

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
            float timeFactor =
                1000 / (1 + Mathf.Pow(10, Time.timeSinceLevelLoad - StopTime + 1));

            _iterator.Iterate(Time.deltaTime * timeFactor);
            _renderer.Render();

            if (Time.timeSinceLevelLoad > StopTime)
                IsRunning = false;
        }
    }
}
