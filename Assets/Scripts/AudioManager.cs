using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [Header("CLASS")]

    [SerializeField]
    private Sound[] sound_list;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sound_list)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
        }
    }

    public void play_sound(string name)
    {
        Sound s = Array.Find(sound_list, sound => sound.name == name);
        s.source.Play();
    }

    public void play_sound_loop(string name)
    {
        Sound s = Array.Find(sound_list, sound => sound.name == name);
        s.source.Play();
        s.source.loop = true;
    }

    public void pause_sound(string name)
    {
        Sound s = Array.Find(sound_list, sound => sound.name == name);
        s.source.Pause();
    }

    public void unpause_sound(string name)
    {
        Sound s = Array.Find(sound_list, sound => sound.name == name);
        s.source.UnPause();
    }

    public void stop_sound(string name)
    {
        Sound s = Array.Find(sound_list, sound => sound.name == name);
        s.source.Stop();
    }

    public bool is_playing(string name)
    {
        Sound s = Array.Find(sound_list, sound => sound.name == name);

        return s.source.isPlaying;
    }
}
