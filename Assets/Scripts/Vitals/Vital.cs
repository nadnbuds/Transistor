using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Create New Vital")]
public class Vital : ScriptableObject
{
    [SerializeField]
    private int _maxHealth;
    public int maxHealth
    {
        get
        {
            return _maxHealth;
        }
        private set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _startingHealth;
    public int startingHealth
    {
        get
        {
            return _startingHealth;
        }
        private set
        {
            _startingHealth = value;
        }
    }

    [SerializeField]
    private int _decrementInterval;
    public int decrementInterval
    {
        get
        {
            return _decrementInterval;
        }
        private set
        {
            _decrementInterval = value;
        }
    }
}