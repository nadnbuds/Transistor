using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubVital : Vital
{
    private IEnumerable<Vital> vitals;

    public SubVital(IEnumerable<Vital> effectedVitals)
    {
        this.vitals = effectedVitals;
    }

    public override abstract void Increment();
    public override abstract void Decrement();
}