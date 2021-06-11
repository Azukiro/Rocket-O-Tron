using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    // Sound name
    public string name;

    // Sound clip
    public AudioClip clip;

    // Sound volume
    [Range(0f, 1f)]
    public float volume;

    // Sound pitch
    [Range(.1f, 3f)]
    public float pitch;

    // Sound name
    public bool loop;

    // AudioSource instance
    [HideInInspector]
    private AudioSource source;

    // Instanciate an AudioSource
    public void InitSound(AudioSource audioSource)
    {
        source = audioSource;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
    }

    // Play the song
    public void Play()
    {
        source.Stop();
        source.Play();
    }
}