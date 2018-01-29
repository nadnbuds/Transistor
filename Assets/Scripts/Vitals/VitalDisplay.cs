using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitalDisplay : MonoBehaviour
{
    [SerializeField]
    public Image vitalStatus;

    [Space(10)]

    [SerializeField]
    public Sprite decreaseStatus;

    [SerializeField]
    public Sprite increaseStatus;

    /// <summary>
    /// Parent rect
    /// </summary>
    private RectTransform _parentRect = null;
    private RectTransform parentRect
    {
        get
        {
            if (_parentRect == null)
                _parentRect = GetComponentInParent<RectTransform>();

            return _parentRect;
        }
    }

    private float maxFillHeight;


    [Range(0f, 1f)]
    private float currentPercentage;

    /// <summary>
    /// This rect transform
    /// </summary>
    private RectTransform _rt = null;
    private RectTransform rt
    {
        get
        {
            if (_rt == null)
                _rt = GetComponent<RectTransform>();

            return _rt;
        }
    }


    private void Awake()
    {
        maxFillHeight = parentRect.sizeDelta.y;
        currentPercentage = rt.sizeDelta.y / maxFillHeight;

        //print("set to " + maxFillHeight);
    }

    /// <summary>
    /// Set fill
    /// </summary>
    public void ModifyPercentage(float p)
    {
        float newFill = p * maxFillHeight;
        //print(maxFillHeight);
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, newFill);

        float diff = currentPercentage - p;
        if (diff > 0 && vitalStatus != null)
        {
            vitalStatus.sprite = decreaseStatus;     }
        else
        {
            vitalStatus.sprite = increaseStatus;
        }
    }
}
