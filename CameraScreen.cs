using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreen : MonoBehaviour
{
    public Camera backgroundCam;
    public Camera mainCam;
    public float targetAspectRatio = 1f;
    public RectTransform UI;
    public RectTransform MainUI;
    bool set;
    float target;

    private void Start()
    {
        target = targetAspectRatio;
        if (mainCam == null)
        {
            mainCam = Camera.main;
            if (mainCam == null)
                mainCam = GetComponent<Camera>();
        }
        if (backgroundCam == null)
        {
            foreach (var cam in Camera.allCameras)
            {
                if (cam != mainCam)
                {
                    backgroundCam = cam;
                    break;
                }
            }
            if (backgroundCam == null)
            {
                backgroundCam = new GameObject("BackgroundCam").AddComponent<Camera>();
                backgroundCam.backgroundColor = Color.black;
            }
            backgroundCam.depth = mainCam.depth - 1;
        }
    }
    private void Update()
    {
        float w = Screen.width;
        float h = Screen.height;
        float a = w / h;
        float scale = a / target;
        Rect r;
        if (a > targetAspectRatio)
        {
            float tw = h * targetAspectRatio;
            float o = (w - tw) * 0.5f;
            r = new Rect(o, 0, tw, h);
            if (scale != 1)
            {
                UI.localScale /= scale;
                target = a;
                set = true;
            }
        }
        else
        {
            float th = w / targetAspectRatio;
            float o = (h - th) * 0.5f;
            r = new Rect(0, o, w, th);
        }
        mainCam.pixelRect = r;
    }
}
