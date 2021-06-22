using UnityEngine.Audio;
using UnityEngine;

/// Class that allows to play a Soung thanks to an AudioClip
/// Can represent a Sound or a Song
[System.Serializable]
public class Sound
{
    /// Sound name
    public string _Name;

    /// Sound clip
    public AudioClip _Clip;

    /// Sound volume
    [Range(0f, 1f)]
    public float _Volume;

    /// Sound pitch
    [Range(.1f, 3f)]
    public float _Pitch;

    /// Sound name
    public bool _Loop;

    /// AudioSource instance
    [HideInInspector]
    private AudioSource _Source;

    /// <summary>
    ///     Instanciate an AudioSource
    /// </summary>
    ///
    /// <param name="audioSource">
    ///     The AudioSource to instanciate
    /// </param>
    public void InitSound(AudioSource audioSource)
    {
        _Source = audioSource;
        _Source.clip = _Clip;
        _Source.volume = _Volume;
        _Source.pitch = _Pitch;
        _Source.loop = _Loop;
    }

    /// <summary>
    ///     Stop and play the AudioSource
    /// </summary>
    public void Play()
    {
        _Source.Stop();
        _Source.Play();
    }
}