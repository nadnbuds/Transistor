using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// vitals effected on right
public class EyeMonitor : MonoBehaviour
{
    public Animator EyeAnimator;
    public Image leftEye;
    public Image rightEye;

    private void Start()
    {
        GameManager.Instance.OnEventChange.AddListener(DisplayEvent);
    }

    public void DisplayEvent()
    {
        print("HERE");
        EyeAnimator.SetTrigger("OpenEyes");
        leftEye.sprite = GameManager.Instance.CurrentEvent.Image;
        rightEye.sprite = GameManager.Instance.CurrentEvent.Image;
    }
}