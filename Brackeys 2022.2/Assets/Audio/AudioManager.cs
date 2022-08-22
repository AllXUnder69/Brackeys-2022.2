using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sounds;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.spatialBlend = (s.environment3D) ? 1 : 0;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound s in sounds.Where(sound => sound.playOnRandom))
        {
            StartCoroutine(PlayOnRandom(s.name));
        }
    }
    void Start()
    {
        //Put the Audio Clips you want to be played at the start of the game
        foreach (Sound s in sounds.Where(sound => sound.playOnAwake))
        {
            Play(s.name);
        }
    }
    void OnValidate()
    {
        foreach (Sound s in sounds)
        {
            try
            {
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
            catch { return; }
        }
    }

    IEnumerator PlayOnRandom(string name)
    {
        float waitTime = UnityEngine.Random.Range(1f, 60f);

        Play(name);

        yield return new WaitForSeconds(waitTime);
        StartCoroutine(PlayOnRandom(name));
    }

    public void Play(string name)
    {
        float delay = 0;
        //Find and Return the sound in the declared sounds array by its name
        Sound s = Array.Find(sounds, sound => sound.name == name);

        //Return error if no sound with that name is found in the sounds array
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Not Found!");
            return;
        }

        //Play the sound using its source
        if (delay == 0)
            s.source.Play();
        else
            s.source.PlayDelayed(delay);
    }
    public void PlayPos(string name, Vector3 pos)
    {
        //Find and Return the sound in the declared sounds array by its name
        Sound s = Array.Find(sounds, sound => sound.name == name);

        //Return error if no sound with that name is found in the sounds array
        if (s == null)
        {
            Debug.LogWarning("Sound: " + s.name + " Not Found!");
            return;
        }

        //Play the sound using its source on a given world position
        AudioSource.PlayClipAtPoint(s.source.clip, pos);
    }
    public void Stop(string name)
    {
        //Find and Return the sound in the declared sounds array by its name
        Sound s = Array.Find(sounds, sound => sound.name == name);

        //Return error if no sound with that name is found in the sounds array
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Not Found!");
            return;
        }

        //Play the sound using its source
        s.source.Stop();
    }
    public AudioSource GetSource(string audioName)
    {
        return Array.Find(sounds, sound => sound.name == audioName).source;
    }
}