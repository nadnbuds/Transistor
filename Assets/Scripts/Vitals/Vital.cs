using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu()]
public class Vital : ScriptableObject
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int startingHealth;

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
        }
    }

    private void Awake()
    {
        Health = startingHealth;
    }

    public void Regulate()
    {
        // TODO
    }
}