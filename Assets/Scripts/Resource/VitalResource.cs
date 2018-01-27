using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType { Heart, Breathing, Digestion, Thought, Heat, All, MAX }

public abstract class VitalResource : MonoBehaviour, Interactable
{
    [SerializeField]
    protected ResourceType resourceType;
    [SerializeField]
    protected int quantity;
    [SerializeField]
    protected float dropForce = 3;

    public ResourceType Type { get; protected set; }
    public int Quantity { get; protected set; }

    public abstract void ToggleInteract(PlayerController player);

    protected void Init()
    {
        Type = resourceType;
        Quantity = quantity;
    }
}
