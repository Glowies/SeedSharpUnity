using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraResizer : MonoBehaviour
{
    // Set this to the in-world distance between the left & right edges of your scene.
    public float sceneWidth = 12;

    private Camera _camera;

    void Start()
    {
        TryGetComponent(out _camera);
    }

    // Adjust the camera's height so the desired scene width fits in view
    // even if the screen/window size changes dynamically.
    void Update()
    {
        float camSize = sceneWidth;

        float aspectRatio = Screen.width / (float)Screen.height;
        if (aspectRatio < 1)
        {
            camSize /= aspectRatio;
        }

        _camera.orthographicSize = camSize;
        _camera.transform.localPosition = new Vector3(0, camSize, -10);
    }
}
