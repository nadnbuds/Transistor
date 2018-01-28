using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Controls information about a Audio Source Group, or a list of audio sources belonging to an Audio Mixer Group
/// </summary>
public class AudioSourceGroup : MonoBehaviour
{
    /// <summary>
    /// Number of starting sources
    /// </summary>
    public int initialSources = 0;

    public AudioMixerGroup audioMixerGroup;

    public ObjectPooler positionalSourcePooler { get; private set; }

    private List<AudioSource> standardSources = new List<AudioSource>();

    private AudioSource blueprint_audiosource;
    private void Awake()
    {
        GameObject blueprint_obj = new GameObject("Positional Source");
        blueprint_obj.SetActive(false);
        blueprint_obj.transform.SetParent(transform);
        blueprint_obj.AddComponent<AudioSource>();
        blueprint_audiosource = blueprint_obj.GetComponent<AudioSource>();
    }

    private void Start()
    {
        //Set up positional source
        gameObject.AddComponent<ObjectPooler>();
        positionalSourcePooler = GetComponent<ObjectPooler>();

        //Set positional source values
        blueprint_audiosource.outputAudioMixerGroup = audioMixerGroup;
        blueprint_audiosource.loop = false;
        blueprint_audiosource.playOnAwake = false;
        blueprint_audiosource.spatialBlend = 1f;
        blueprint_audiosource.minDistance = AudioManager.Instance.minDistance;
        blueprint_audiosource.maxDistance = AudioManager.Instance.maxDistance;
        blueprint_audiosource.rolloffMode = AudioManager.Instance.rollOffMode;

        positionalSourcePooler.InitPooler(blueprint_audiosource.gameObject, transform, initialSources);

        //Set up standard channels
        for (int i = 0; i < initialSources; ++i)
        {
            CreateStandardAudioSource();
        }
    }

    /// <summary>
    /// Return the next avaliable source
    /// </summary>
    public AudioSource GetNextAvaliableSource()
    {
        foreach(AudioSource channel in standardSources)
        {
            if (!channel.isPlaying)
                return channel;
        }
        return CreateStandardAudioSource();
    }

    /// <summary>
    /// Set volume for all sources in this group
    /// </summary>
    public void SetVolume(float volume)
    {
        foreach(AudioSource channel in standardSources)
        {
            channel.volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }

    /// <summary>
    /// Stop all audio from all sources
    /// </summary>
    public void StopAllAudio()
    {
        foreach (AudioSource source in standardSources)
        {
            source.Stop();
            source.loop = false;
        }
    }

    /// <summary>
    /// Pause or unpause all audio from all sources
    /// </summary>
    public void PauseAllAudio(bool pause)
    {
        if (pause)
        {
            foreach (AudioSource source in standardSources)
            {
                source.Pause();
            }
        }
        else
        {
            foreach (AudioSource source in standardSources)
            {
                source.UnPause();
            }
        }
    }

    /// <summary>
    /// Create new standard audio source
    /// </summary>
    private AudioSource CreateStandardAudioSource()
    {
        AudioSource new_source = gameObject.AddComponent<AudioSource>();
        new_source.playOnAwake = false;
        new_source.loop = false;
        new_source.outputAudioMixerGroup = audioMixerGroup;
        standardSources.Add(new_source);

        return new_source;
    }
}
