using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalDisplay : MonoBehaviour
{
    /// <summary>
    /// Max fill height
    /// </summary>
    private float GetMaxFill;

    /// <summary>
    /// This rect transform
    /// </summary>
    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        GetMaxFill = GetComponentInParent<RectTransform>().rect.height;
    }   

    /// <summary>
    /// Set fill
    /// </summary>
    public void ModifyPercentage(float p)
    {
        float newFill = p * GetMaxFill;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, newFill);
    }
}
