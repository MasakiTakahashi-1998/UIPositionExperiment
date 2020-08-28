using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FixResolution_RenderTexture : MonoBehaviour
{
    Vector2 nowResolution = Vector2.zero;

    [SerializeField] public Canvas mainCanvas;
    [SerializeField] public RawImage rawImage;
    [SerializeField] public Camera targetCamera;
    [SerializeField] private GraphicsFormat _graphics = GraphicsFormat.None;
    [SerializeField] private DepthBuffer _depthBuffer = DepthBuffer.Twentyfour;
    
    private GraphicsFormat nowgraphics = GraphicsFormat.None;
    private DepthBuffer nowdepthBuffer = DepthBuffer.None;

    void Update()
    {
        if (nowResolution != mainCanvas.GetComponent<RectTransform>().sizeDelta || nowgraphics != _graphics || nowdepthBuffer != _depthBuffer)
        {
            nowgraphics = _graphics;
            nowdepthBuffer = _depthBuffer;
            nowResolution = mainCanvas.GetComponent<RectTransform>().sizeDelta;
            
            if (targetCamera.targetTexture != null)
            {
                targetCamera.targetTexture.Release();
            }

            targetCamera.targetTexture = new RenderTexture((int) nowResolution.x, (int)nowResolution.y, (int)nowdepthBuffer, nowgraphics)
            {
                name = $"{(int) nowResolution.x}x{(int) nowResolution.y}"
            };
            rawImage.texture = targetCamera.targetTexture;
        }
    }

    enum DepthBuffer
    {
        None = 0,
        Sixteen = 16,
        Twentyfour = 24,
    }
}
