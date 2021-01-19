using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weather Preset", menuName = "Weather/Weather Preset")]
public class WeatherPreset : ScriptableObject
{
    public string name;

    public LightingPreset lightingPreset;
    public Transform weatherVfx;
    public Sound ambientSound;
}
