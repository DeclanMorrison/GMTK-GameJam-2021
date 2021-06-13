using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;
    public AudioSource source;
    public List<SoundEffect> allSoundEffects = new List<SoundEffect>();

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Play(string name)
    {
        bool effectFound = false;
        foreach(SoundEffect se in allSoundEffects)
        {
            if(name == se.name)
            {
                source.PlayOneShot(se.clips[UnityEngine.Random.Range(0, se.clips.Length)], se.volume);
                effectFound = true;
                continue;
            }
        }
        if(effectFound == false)
        {
            Debug.Log("Couldn't find clip: " + name);
        }
    }

    [Serializable]
    public class SoundEffect
    {
        public string name;
        public AudioClip[] clips = new AudioClip[1]; //clips(will randomly chose 1)
        [Range(0,1)]public float volume;
    }
}
