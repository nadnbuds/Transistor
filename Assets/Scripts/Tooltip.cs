using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private float height = 10f;

    /// <summary>
    /// Show tooltip above gameobject
    /// </summary>
    /// <param name="g"></param>
    public void ShowTooltip(Transform t)
    {
        transform.gameObject.SetActive(true);
        //Show tooltip above object
        transform.position = t.position + Vector3.up * height;
    }

    /// <summary>
    /// Hide the tooltip
    /// </summary>
    public void HideTooltip()
    {
        transform.gameObject.SetActive(false);
    }
}
