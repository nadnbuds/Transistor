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
    private float GetMaxFill;

    [Range(0f, 1f)]
    private float currentPercentage;

    /// <summary>
    /// This rect transform
    /// </summary>
    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        GetMaxFill = GetComponentInParent<RectTransform>().rect.height;
        currentPercentage = GetComponent<RectTransform>().rect.height / GetMaxFill;
    }   

    /// <summary>
    /// Set fill
    /// </summary>
    public void ModifyPercentage(float p)
    {
        float newFill = p * GetMaxFill;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, newFill);

        float diff = currentPercentage - p;
        if (diff > 0)
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
