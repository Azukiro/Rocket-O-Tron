using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    private static AudioManager m_Instance;

    public static AudioManager Instance
    {
        get { return m_Instance; }
        private set { }
    }

    private void Awake()
    {
        if (!m_Instance)
        {
            m_Instance = this;
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion Singleton

    // Unity variables
    [SerializeField] private Sound[] sounds;

    [SerializeField] private Sound[] songs;

    // Song index
    private int songIndex = 0;

    // Create an AudioSource for each sound and songs
    private void Init()
    {
        // Use of an Action<Sound> to respect the DRY norm
        Action<Sound> initSound = sound => sound.InitSound(gameObject.AddComponent<AudioSource>());
        foreach (Sound sound in sounds) initSound(sound);
        foreach (Sound song in songs) initSound(song);
    }

    private void Start()
    {
        // Play the first song of the Playlist
        PlaySong();
    }

    // Play the next song of the playlist
    private void PlaySong()
    {
        // Empty playlist
        if (songs.Length == 0)
            return;

        // Play the next song when the current song ended
        float songTime = songs[songIndex].clip.length;
        StartCoroutine(
            Util.ExecuteAfterTime(songTime, () =>
            {
                // Play the next song
                songIndex = (songIndex + 1) % (songs.Length);
                PlaySong();
            })
        );
        songs[songIndex].Play();
    }

    // Play an AudioSource by name
    // Example : AudioManager.instance.Play("Sound Name");
    public void Play(string soundName)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }
        sound.Play();
    }
}