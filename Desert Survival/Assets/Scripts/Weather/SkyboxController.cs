using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material currentSkybox;
    private void Awake()
    {
        if(currentSkybox != null)
        {
            RenderSettings.skybox = currentSkybox;
        }
    }
}
