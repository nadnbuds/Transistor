﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VitalBehavior : MonoBehaviour
{
    [SerializeField]
    private Vital vitalData;

    [SerializeField]
    private VitalDisplay ui;

    private int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health > vitalData.maxHealth)
            {
                health = vitalData.maxHealth;
            }
            if (health <= 0)
            {
                health = 0;
                OnZeroHealth();
            }

            //Update ui
            //print(Health + " / " + vitalData.maxHealth);
            ui.ModifyPercentage(Health / (float)vitalData.maxHealth);
        }
    }

    public abstract ResourceType GetCompatibleType();

    public void ParseResource(VitalResource r)
    {
        Health += r.Quantity;
    }

    public float DecrementInterval {
		get { return vitalData.decrementInterval; }
		set { vitalData.decrementInterval = (int)value;}
		//set { DecrementInterval = value;}
    }



    /// <summary>
    /// Event that triggers when vital health completely depletes
    /// </summary>
    protected virtual void OnZeroHealth()
    {
        GameManager.Instance.GameOver();
    }

    private void Start()
    {
        Health = vitalData.startingHealth;
    }
}
