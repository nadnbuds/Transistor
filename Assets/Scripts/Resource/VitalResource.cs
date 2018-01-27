using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType { Heart, Breathing, Digestion, Thought, Heat, All, MAX }

public abstract class VitalResource : MonoBehaviour
{
    [SerializeField]
    protected ResourceType resourceType;
    [SerializeField]
    protected int quantity;

    public ResourceType Type { get; protected set; }
    public int Quantity { get; protected set; }
    
    protected void Init()
    {
        Type = resourceType;
        Quantity = quantity;
    }
}
