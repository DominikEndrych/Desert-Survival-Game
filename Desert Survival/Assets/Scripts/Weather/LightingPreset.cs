using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="Lighting Preset", menuName = "Weather/Lighting Preset")]
public class LightingPreset : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;

    //public Material SkyboxMaterial;
    [Header("Skybox")]
    [Range(0,1)] public float sunSize;
    [Range(0,10)] public float sunSizeConvergence;
    [Range(0, 5)] public float atmosphereThickness;
    public Color skyTint;
    [Range(0,8)] public float exposure;
}
