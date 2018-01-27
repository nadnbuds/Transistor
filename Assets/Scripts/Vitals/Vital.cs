using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum VitalType{ BodyHeat, Digestion, HeartBeat, Thought, Energy, Exercise, Mood, MAX }

[CreateAssetMenu()]
public class Vital : ScriptableObject
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int startingHealth;
    [SerializeField]
    private VitalType vitalType;
    [SerializeField]
    private List<ResourceType> acceptableResources;

    public UnityEvent OnZeroHealth { get; private set; }
    private int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health += value;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            if (health <= 0)
            {
                health = 0;
                OnZeroHealth.Invoke();
            }
        }
    }

    private void Awake()
    {
        Health = startingHealth;
    }

    public bool ParseResource(VitalResource res)
    {
        if (acceptableResources.Contains(res.Type))
        {
            Health += res.Quantity;
            return true;
        }
        return false;
    }


}