using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingController : MonoBehaviour
{
    [SerializeField] Light DirectionalLight;
    public LightingPreset Preset;

    [SerializeField, Range(0, 24)] float TimeOfDay;

    private void Update()
    {
        if(Preset != null)
        {
            if (Application.isPlaying)
            {
                TimeOfDay += Time.deltaTime / 36f;
                TimeOfDay %= 24;
                UpdateLighting(TimeOfDay / 24f);
            }
            else
            {
                UpdateLighting(TimeOfDay / 24f);
            }
        }
        else { return; }
    }

    private void UpdateLighting(float currentTime)
    {
        //RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(currentTime);
        RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, Preset.AmbientColor.Evaluate(currentTime), 0.007f);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(currentTime);
        //RenderSettings.skybox = Preset.SkyboxMaterial;

        if(DirectionalLight != null)
        {
            //DirectionalLight.color = Preset.DirectionalColor.Evaluate(currentTime);
            DirectionalLight.color = Color.Lerp(DirectionalLight.color, Preset.DirectionalColor.Evaluate(currentTime), 0.007f);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((currentTime * 360f) - 90f - 170f, 0));
        }
    }
}
