using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{

    [SerializeField] string soundName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Stop();
        }
    }

    private void Play()
    {
        AudioManager.instance.PlayFadeIn(soundName);
    }

    private void Stop()
    {
        AudioManager.instance.StopFadeOut(soundName);
    }
}
