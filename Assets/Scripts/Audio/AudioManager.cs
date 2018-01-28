using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Central location for playing all audio and which channel to play the audio from
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    protected AudioManager() { }


    /// <summary>
    /// Attach reference to primary Mixer.
    /// </summary>
    [Tooltip("Attach reference to primary Mixer.")]
    public AudioMixer mainAudioMixer = null;

    /// <summary>
    /// Mute all audio at start
    /// </summary>
    [Tooltip("If true, mute all audio at game start")]
    [SerializeField]
    private bool muteAtStart = false;

    /// <summary>
    /// Starting number of sources for all audio types
    /// </summary>
    [Tooltip("Starting number of sources for all audio types.")]
    [SerializeField]
    private int startingSources = 10;

    /// <summary>
    /// 3D audio roll off mode
    /// </summary>
    public AudioRolloffMode rollOffMode;

    /// <summary>
    /// 3D audio max distance
    /// </summary>
    public float maxDistance;

    /// <summary>
    /// 3D audio min distance
    /// </summary>
    public float minDistance;

    /// <summary>
    /// Store all source groups
    /// </summary>
    private Dictionary<AudioMixerGroup, AudioSourceGroup> allSourceGroups = new Dictionary<AudioMixerGroup, AudioSourceGroup>();

    /// <summary>
    /// Set to true if audio manager will not function properly
    /// </summary>
    private bool Disabled = false;

    private void Awake()
    {
        if (mainAudioMixer == null)
        {
            Debug.LogWarning("Main Audio Mixer not set. You probably need to include the Audio Manager prefab into your scene. Audio will be disabled.");
            Disabled = true;
            return;
        }

        AudioMixerGroup[] groups = mainAudioMixer.FindMatchingGroups("Master");

        foreach (AudioMixerGroup mixer_group in groups)
        {
            //print(mixer_group.name);

            GameObject source_obj = new GameObject();
            source_obj.AddComponent<AudioSourceGroup>();
            AudioSourceGroup audio_source_group = source_obj.GetComponent<AudioSourceGroup>();

            audio_source_group.initialSources = startingSources;
            audio_source_group.audioMixerGroup = mixer_group;
            source_obj.transform.SetParent(transform);
            source_obj.name = mixer_group.name;
            allSourceGroups.Add(mixer_group, audio_source_group);
        }
    }

    private void Start()
    {
        //Mute audio if necessary
        SetGlobalMute(muteAtStart);
    }

    /// <summary>
    /// Play audio once
    /// </summary>
    public void PlayAudioSource(AudioInfo s)
    {
        if (s == null || Disabled)
            return;

        AudioSource source = allSourceGroups[s.mixerGroup].GetNextAvaliableSource();
        source.volume = s.volumePercentage;
        source.clip = s.Clip;
        source.loop = false;
        source.Play();
    }

    /// <summary>
    /// Play in a loop
    /// Returns the audio source
    /// </summary>
    public AudioSource PlayLoopingAudioSource(AudioInfo s)
    {
        if (s == null || Disabled)
            return null;

        AudioSource source = allSourceGroups[s.mixerGroup].GetNextAvaliableSource();
        source.volume = s.volumePercentage;
        source.clip = s.Clip;
        source.loop = true;
        source.Play();
        return source;
    }

    
    /// <summary>
    /// Play Audio at gameobject's position
    /// </summary>
    public void PlayAudioAtPoint(AudioInfo s, Vector3 pos)
    { 
        if (s == null || Disabled)
            return;

        GameObject positional_source = allSourceGroups[s.mixerGroup].positionalSourcePooler.RetrieveCopy();
        positional_source.transform.position = pos;
        AudioSource source = positional_source.GetComponent<AudioSource>();
        source.volume = s.volumePercentage;
        source.clip = s.Clip;
        source.loop = false;
        source.Play();
        StartCoroutine(deactivateObj(positional_source, source.clip.length));
    }

    /// <summary>
    /// Play Looping Audio at gameobject's position
    /// </summary>
    public AudioSource PlayLoopingAudioAtPoint(AudioInfo s, Vector3 pos)
    {
        if (s == null || Disabled)
            return null;

        GameObject positional_source = allSourceGroups[s.mixerGroup].positionalSourcePooler.RetrieveCopy();
        positional_source.transform.position = pos;
        AudioSource source = positional_source.GetComponent<AudioSource>();
        source.volume = s.volumePercentage;
        source.clip = s.Clip;
        source.loop = true;
        source.Play();

        return source;
    }

    /// <summary>
    /// Deactivates obj in specified seconds
    /// </summary>
    IEnumerator deactivateObj(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }

    /// <summary>
    /// Stop looping for an audio source
    /// </summary>
    public void StopLooping(AudioSource s)
    {
        if (Disabled)
            return;

        s.loop = false;

        //If positional sound, deactivate game objects
        if (s.spatialBlend == 1f)
            s.gameObject.SetActive(false);
    }

    /// <summary>
    /// Stop a source group
    /// </summary>
    public void MuteSourceGroup(AudioMixerGroup id)
    {
        if (Disabled)
            return;

        allSourceGroups[id].StopAllAudio();
    }

    /// <summary>
    /// Pause or unpause all source groups
    /// </summary>
    public void PauseAllSourceGroups(bool pause)
    {
        if (Disabled)
            return;

        foreach(KeyValuePair<AudioMixerGroup, AudioSourceGroup> pair in allSourceGroups)
        {
            pair.Value.PauseAllAudio(pause);
        }
    }

    /// <summary>
    /// Toggle global mute
    /// </summary>
    public void ToggleGlobalMute()
    {
        SetGlobalMute(!IsGlobalMute());
    }

    /// <summary>
    /// Set to global mute all or not
    /// </summary>
    private void SetGlobalMute(bool mute)
    {
        if (mute)
            mainAudioMixer.SetFloat("Volume", -80f);
        else
            mainAudioMixer.SetFloat("Volume", 0f);
    }

    /// <summary>
    /// Returns true if global muted
    /// </summary>
    public bool IsGlobalMute()
    {
        float volume;
        mainAudioMixer.GetFloat("Volume", out volume);
        return volume == -80f;
    }
}