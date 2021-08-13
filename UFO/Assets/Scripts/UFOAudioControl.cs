using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOAudioControl : MonoBehaviour
{
    public AudioSource[] audioSources;
    public Dictionary <string, AudioSource> dictionaryAudioSources = new Dictionary<string, AudioSource>();

    
    public string nameKeyForEngineWork = "Engine";
    public string nameKeyForExploision = "Exploision";
    public string nameKeyForFire = "FireExploision";

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();

        InitDictianory();
    }

    private void InitDictianory()
    {
        dictionaryAudioSources.Add(nameKeyForEngineWork, audioSources[0]);
        dictionaryAudioSources.Add(nameKeyForExploision, audioSources[1]);
        dictionaryAudioSources.Add(nameKeyForFire, audioSources[2]);
    }


    public void PlaySound(string nameSound)
    {
        if(!dictionaryAudioSources[nameSound].isPlaying)
            dictionaryAudioSources[nameSound].Play();
        if (nameSound.Equals(nameKeyForExploision))
        {
            PlaySound(nameKeyForFire);
        } 
    }

    public void PauseSound(string nameSound)
    {
        dictionaryAudioSources[nameSound].Pause();
    }

    public void StopSound(string nameSound)
    {
        dictionaryAudioSources[nameSound].Stop();
    }
}
