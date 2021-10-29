using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionEffectScript : MonoBehaviour
{
    public Material material;
    void OnRenderImage(RenderTexture scr, RenderTexture dest){
        Graphics.Blit(scr, dest, material);
    }
}
