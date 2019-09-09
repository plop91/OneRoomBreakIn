using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool mute;
    public Sound[] sounds;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        if (!mute)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s.loop)
            {
                s.source.loop = true; ;
            }
            s.source.Play();
        }
    }
    public void StopAll()
    {
        foreach(Sound s in sounds)
        {
            s.source.Stop();
        }
    }
}
