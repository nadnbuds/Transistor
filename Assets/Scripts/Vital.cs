﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Vital
{
    public abstract void Increment(float value);
    public abstract void Decrement(float value);
}