using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Producer : MonoBehaviour
{
    [SerializeField]
    protected float dropRadiusMin, dropRadiusMax;
    public ObjectPooler pooler;
    public float SpawnInterval { get; protected set; }

	public abstract void Generate();
    
    protected void RegisterProducer()
    {
        GameManager.Instance.AddProducer(this);
    }

    protected Vector3 RandDrop()
    {
        Vector2 Location = Random.insideUnitCircle * (dropRadiusMax - dropRadiusMin);
        float angle = Vector2.Angle(Vector2.zero, Location);
        angle = Location.y < 0 ? 360 - angle : angle;
        angle *= Mathf.Deg2Rad;
        Location.x += Mathf.Cos(angle) * dropRadiusMin;
        Location.y += Mathf.Sin(angle) * dropRadiusMin;
        return new Vector3(Location.x, 0.0f, Location.y);
    }
}
