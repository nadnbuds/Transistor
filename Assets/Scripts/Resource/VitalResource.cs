using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType { Heart, Digestion, Thought, All, MAX }

public abstract class VitalResource : MonoBehaviour, Interactable
{
    [SerializeField]
    protected ResourceType resourceType;
    [SerializeField]
    protected int quantity;
    [SerializeField]
    protected float dropForce = 3;

    protected Rigidbody rb;
    protected Collider col;

    public ResourceType Type { get; protected set; }
    public int Quantity { get; protected set; }

    public abstract void ToggleInteract(PlayerController player);

    protected void Init()
    {
        Type = resourceType;
        Quantity = quantity;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
}
