using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    private AudioSource[] selfSources;
    private Dictionary<string, AudioSource> activeSources;

    private void Awake()
    {

        selfSources = gameObject.GetComponents<AudioSource>();
        activeSources = new Dictionary<string, AudioSource>();

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        /*
        foreach(Sound sound in sounds)
        {
            sound.source = selfSource;
            sound.source.clip = sound.clip;
        }

        selfSource.clip = null;
        */
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s != null)
        {
            s.source = GetEmptySource();
            if(s.source != null)
            {
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.loop = s.loop;
                activeSources.Add(name, s.source);
                s.source.Play();
            }
        }
        else
        {
            return;
        }
        
    }

    public void PlayFadeIn(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            s.source = GetEmptySource();
            if(s.source != null)
            {
                s.source.clip = s.clip;
                s.source.volume = 0f;
                s.source.loop = s.loop;
                activeSources.Add(name, s.source);
                s.source.Play();
                StartCoroutine(FadeIn(s.source, s.volume));
            }
        }
        else
        {
            return;
        }
    }

    public void PlayFadeIn(Sound s)
    {
        if (s != null)
        {
            s.source = GetEmptySource();
            if (s.source != null)
            {
                s.source.clip = s.clip;
                s.source.volume = 0f;
                s.source.loop = s.loop;
                activeSources.Add(s.name, s.source);
                s.source.Play();
                StartCoroutine(FadeIn(s.source, s.volume));
            }
        }
        else
        {
            return;
        }
    }

    public void PlaySound(Sound sound)
    {
        if(sound != null)
        {
            sound.source = GetEmptySource();
            if(sound.source != null)
            {
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.loop = sound.loop;
                activeSources.Add(sound.name, sound.source);
                sound.source.Play();
            }
            
        }
    }

    public void Stop(string name)
    {

        AudioSource source = activeSources[name];
        source.Stop();
        source.clip = null;
        activeSources.Remove(name);
    }

    public void StopFadeOut(string name)
    {
        AudioSource source = activeSources[name];
        instance.activeSources.Remove(name);
        StartCoroutine(FadeOut(source, name, source.volume));
        
    }

    private AudioSource GetEmptySource()
    {
        foreach(AudioSource source in selfSources)
        {
            if(source.clip == null)
            {
                return source;
            }
        }
        return null;
    }

    private static IEnumerator FadeIn(AudioSource source, float desiredVolume)
    {
        float fadeTime = 1.2f;
        while (source.volume < desiredVolume)
        {
            source.volume += desiredVolume * Time.deltaTime / fadeTime;

            yield return null;
        }
        Debug.Log("konec fade in");
    }

    private static IEnumerator FadeOut(AudioSource source, string name, float startVolume)
    {
        float fadeTime = 1.2f;
        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }
        source.Stop();
        source.clip = null;
        
    } 
}
