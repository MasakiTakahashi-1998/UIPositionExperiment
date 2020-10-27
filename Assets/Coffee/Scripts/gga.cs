using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class gga : MonoBehaviour
{
    public List<Camera> cameras;

    // Update is called once per frame
    void Update()
    {
        SetCameraClearFlags();
    }
    
    private void SetCameraClearFlags()
    {
        cameras = FindObjectsOfType<Camera>().Where(x => x.targetTexture == null).ToList();
        var maxDepth = cameras.Min(x => x.depth);

        foreach (var value in cameras)
        {
            if (Math.Abs(maxDepth - value.depth) > 0)
            {
                value.clearFlags = CameraClearFlags.Depth;
                continue;
            }
            value.clearFlags = CameraClearFlags.SolidColor;
            value.backgroundColor = Color.white;
        }
    }
}
