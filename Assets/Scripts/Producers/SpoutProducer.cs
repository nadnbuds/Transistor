using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoutProducer : Producer
{
    [SerializeField]
    private float forceStrength = 3;

    private void Awake()
    {
        SpawnInterval = 1f;
        RegisterProducer();
    }

    public override void Generate()
    {
        Vector3 dir = RandDrop();
        VitalResource res = Instantiate(resource);
        res.transform.position = dir + transform.position;
        Rigidbody rb = res.GetComponent<Rigidbody>();
        rb.AddForce(dir * forceStrength);
    }
}
