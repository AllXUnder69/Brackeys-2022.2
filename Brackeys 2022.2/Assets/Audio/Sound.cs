using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume = 1f;
    [Range(0.1f, 3)]
    public float pitch = 1f;

    public bool playOnAwake = false;
    public bool playOnRandom = false;
    public bool environment3D = false;
    public bool loop = false;
    public bool DDOL = false;

    [HideInInspector]
    public AudioSource source;
}