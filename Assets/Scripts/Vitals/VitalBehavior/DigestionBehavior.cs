﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigestionBehavior : VitalBehavior
{
    public override ResourceType GetCompatibleType()
    {
        return ResourceType.Digestion;
    }
    /// <summary>
    /// Event that triggers when vital health completely depletes
    /// </summary>
    protected override void OnZeroHealth()
    {
        GameManager.Instance.GameOver();
    }
}
