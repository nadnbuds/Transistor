using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Producer : MonoBehaviour {

	public VitalResource resource;

	protected abstract void generate();

    [SerializeField]
    protected float dropRadiusMin, dropRadiusMax;

    protected Vector3 randDrop()
    {
        Vector2 Location = Random.insideUnitCircle * (dropRadiusMax - dropRadiusMin);
        float angle = Vector2.Angle(Vector2.zero, Location);
        Vector3 cross = Vector3.Cross(Vector2.zero, Location);
        angle = cross.z > 0 ? 360 - angle : angle;
        angle *= Mathf.Deg2Rad;
        Location.x += Mathf.Cos(angle) * dropRadiusMin;
        Location.y += Mathf.Sin(angle) * dropRadiusMin;
        return new Vector3(Location.x, 0.0f, Location.y);
    }
}
