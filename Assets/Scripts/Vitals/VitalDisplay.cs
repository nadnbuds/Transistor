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
    public Image decreaseStatus;

    [SerializeField]
    public Image veryDecreaseStatus;

    [SerializeField]
    public Image increaseStatus;

    /// <summary>
    /// Max fill height
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
        currentPercentage = GetComponent<RectTransform>().rect.height / parentRect.rect.height;
    }   

    /// <summary>
    /// Set fill
    /// </summary>
    public void ModifyPercentage(float p)
    {
        float newFill = p * parentRect.rect.height;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, newFill);

        float diff = currentPercentage - p;
        if (diff > 0 && vitalStatus != null)
        {
            //Increase
            vitalStatus = increaseStatus;
        }
        else
        {
            //Lower
            vitalStatus = decreaseStatus;
        }
    }
}
