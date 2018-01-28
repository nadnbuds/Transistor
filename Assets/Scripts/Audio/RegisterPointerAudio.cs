using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Play audio for pointer events
/// </summary>
public class RegisterPointerAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private AudioInfo audioPointerEnter;

    [SerializeField]
    private AudioInfo audioPointerExit;

    [SerializeField]
    private AudioInfo audioPointerDown;

    [SerializeField]
    private AudioInfo audioPointerUp;

    /// <summary>
    /// GameObject does not require a button component, it is optional
    /// </summary>
    private Button buttonRef = null;

    private void Awake()
    {
        buttonRef = GetComponent<Button>();
    }

    /// <summary>
    /// Play audio on pointer enter
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonRef != null && !buttonRef.interactable)
            return;

        AudioManager.Instance.PlayAudioSource(audioPointerEnter);
    }

    /// <summary>
    /// Play audio on pointer exit
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonRef != null && !buttonRef.interactable)
            return;

        AudioManager.Instance.PlayAudioSource(audioPointerExit);
    }

    /// <summary>
    /// Play audio on pointer down
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonRef != null && !buttonRef.interactable)
            return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            AudioManager.Instance.PlayAudioSource(audioPointerDown);
        }
    }

    /// <summary>
    /// Play audio on pointer up
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonRef != null && !buttonRef.interactable)
            return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            AudioManager.Instance.PlayAudioSource(audioPointerUp);
        }
    }
}
