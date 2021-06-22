using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// This class is used to play SFX in a easy way
public class AudioManager : MonoBehaviour
{
    #region Singleton

    /// The only one instance of AudioManager
    private static AudioManager _Instance;

    /// The AudioManager property
    public static AudioManager Instance
    {
        get { return _Instance; }
        private set { }
    }

    /// <summary>
    ///     Singleton Pattern
    /// </summary>
    private void Awake()
    {
        if (!_Instance)
        {
            /// If it's the first instance, store it an load all the SFX
            _Instance = this;
            Init();
        }
        else
        {
            /// If an instance already exists, delete the new one
            Destroy(gameObject);
        }
    }

    /// <summary>
    ///     Create an AudioSource for each sound and songs
    /// </summary>
    private void Init()
    {
        /// Use of an Action<Sound> to respect the DRY norm
        Action<Sound> initSound = sound => sound.InitSound(gameObject.AddComponent<AudioSource>());
        foreach (Sound sound in _Sounds) initSound(sound);
        foreach (Sound song in _Songs) initSound(song);
    }

    #endregion Singleton

    #region Sounds

    /// All the sounds of the game
    [SerializeField] private Sound[] _Sounds;

    /// <summary>
    ///     Play an AudioSource by name
    /// </summary>
    ///
    /// <param name="soundName">
    ///     The name of the sound to play
    /// </param>
    public void Play(string soundName)
    {
        /// Get the sound thanks to the _Sounds list
        Sound sound = Array.Find(_Sounds, sound => sound._Name == soundName);

        /// Show a warning if the soung is not exists
        if (sound == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }

        /// Play the sound
        sound.Play();
    }

    #endregion Sounds

    #region Sounds

    /// All the sounds of the game
    [SerializeField] private Sound[] _Songs;

    /// Index of the song of _Songs that is currently playing
    private int songIndex = 0;

    /// <summary>
    ///     Play the firsy song of the sound playlist
    /// </summary>
    private void Start()
    {
        PlaySong();
    }

    /// <summary>
    ///     Play the next song of the playlist
    /// </summary>
    private void PlaySong()
    {
        /// If the playlist, do nothing to avoid errors
        if (_Songs.Length == 0)
            return;

        /// Play the next song when the current song ended
        float songTime = _Songs[songIndex]._Clip.length;
        StartCoroutine(
            Util.ExecuteAfterTime(songTime, () =>
            {
                /// Play the next song
                songIndex = (songIndex + 1) % (_Songs.Length);
                PlaySong();
            })
        );

        /// Play the current song
        _Songs[songIndex].Play();
    }

    #endregion Sounds
}