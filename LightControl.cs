using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    public enum Quality
    {
        High,
        Low,
        Disable
    }
    public Quality quality;

    public bool set;
    GameObject[] lightlow;
    GameObject[] lighthigh;
    public RenderPipelineAsset render;  

    void Start()
    {
        lightlow = GameObject.FindGameObjectsWithTag("LightLow");
        lighthigh = GameObject.FindGameObjectsWithTag("LightHigh");

        switch (quality)
        {
            case Quality.High:
                foreach (GameObject light in lighthigh)
                {
                    light.SetActive(true);
                }
                foreach (GameObject light in lightlow)
                {
                    light.SetActive(false);
                }
                break;
            case Quality.Low:
                foreach (GameObject light in lightlow)
                {
                    light.SetActive(true);
                }
                foreach (GameObject light in lighthigh)
                {
                    light.SetActive(false);
                }
                break;
            case Quality.Disable:
                GraphicsSettings.renderPipelineAsset = render;
                break;
        }
    }

    void Update()
    {
    }
}
