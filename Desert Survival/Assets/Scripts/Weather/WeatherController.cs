﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeatherController : MonoBehaviour
{
    public WeatherPreset currentWeather;
    public WeatherPreset[] presets;

    [SerializeField] float minimalChangingSpeed;
    [SerializeField] Transform weatherVfxParent;

    private LightingController lightingController;
    private float lerpStep = 0.007f;
    public void Start()
    {
        lightingController = gameObject.GetComponent<LightingController>();
        StartCoroutine(WeatherRoutine());
    }

    public void Update()
    {
        UpdateSkybox(currentWeather.lightingPreset);
    }

    IEnumerator WeatherRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(minimalChangingSpeed);
            WeatherPreset newWeather = GetRandomWeather();

            //change weather only if preset is different
            if(newWeather != currentWeather)
            {
                StopCurrentVfx();
                ChangeWeather(newWeather);
            }
        }
    }

    public void ChangeWeather(WeatherPreset preset)
    {
        lightingController.Preset = preset.lightingPreset;
        currentWeather = preset;
        
        //change weather VFX
        if(preset.weatherVfx != null)
        {
            GameObject vfx = GameObject.Instantiate(preset.weatherVfx).gameObject;
            vfx.transform.parent = weatherVfxParent;
            vfx.transform.localPosition = new Vector3(0, preset.weatherVfx.transform.position.y, 0);
        }

        //change ambient sound
        if(preset.ambientSound.clip != null)
        {
            AudioManager.instance.PlayFadeIn(preset.ambientSound);
        }

        Debug.Log("Weather Changed");
    }

    private void UpdateSkybox(LightingPreset preset)
    {
        //lerp skybox properties
        RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(RenderSettings.skybox.GetColor("_SkyTint"), preset.skyTint, lerpStep));
        RenderSettings.skybox.SetFloat("_SunSize", Mathf.Lerp(RenderSettings.skybox.GetFloat("_SunSize"), preset.sunSize, lerpStep));
        RenderSettings.skybox.SetFloat("_SunSizeConvergence", Mathf.Lerp(RenderSettings.skybox.GetFloat("_SunSizeConvergence"), preset.sunSizeConvergence, lerpStep));
        RenderSettings.skybox.SetFloat("_AtmosphereThickness", Mathf.Lerp(RenderSettings.skybox.GetFloat("_AtmosphereThickness"), preset.atmosphereThickness, lerpStep));
        RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(RenderSettings.skybox.GetFloat("_Exposure"), preset.exposure, lerpStep));


    }

    private WeatherPreset GetRandomWeather()
    {
        int index = Random.Range(0,presets.Length);
        return presets[index];
    }

    private void StopCurrentVfx()
    {
        var childernVfx = weatherVfxParent.gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem child in childernVfx)
        {
            GameObject.Destroy(child.gameObject);
        }
        if(currentWeather.ambientSound.clip != null)
        {
            AudioManager.instance.StopFadeOut(currentWeather.ambientSound.name);
        }
    }

}
