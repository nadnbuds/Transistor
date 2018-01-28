using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Wrapper for an audio clip
/// </summary>
[CreateAssetMenu(fileName = "Audio Info", menuName = "Audio/Audio Info", order = 1)]
public class AudioInfo: ScriptableObject
{
    /// <summary>
    /// Which mixer group this audio clip should belong to
    /// </summary>
    public AudioMixerGroup mixerGroup;

    /// <summary>
    /// Audio clip
    /// </summary>
    public AudioClip Clip;

    /// <summary>
    /// Audio clip volume
    /// </summary>
    [Range(0f, 1f)]
    public float volumePercentage = 0.5f;
}
