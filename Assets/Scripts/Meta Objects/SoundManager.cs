using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Serializable]
    private class SoundEffect
    {
        public string id;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
    }

    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Sound Library")]
    [SerializeField] private SoundEffect[] soundEffects;

    private readonly Dictionary<string, SoundEffect> soundLookup = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (SoundEffect sound in soundEffects)
        {
            if (sound.clip != null && !string.IsNullOrWhiteSpace(sound.id))
            {
                soundLookup[sound.id] = sound;
            }
        }
    }

    public void PlaySfx(string id)
    {
        if (!soundLookup.TryGetValue(id, out SoundEffect sound))
        {
            Debug.LogWarning($"No sound effect registered with ID '{id}'.");
            return;
        }

        sfxSource.PlayOneShot(sound.clip, sound.volume);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null)
        {
            return;
        }

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
        musicSource.clip = null;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }
}