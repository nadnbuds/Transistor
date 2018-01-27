using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoutProducer : Producer
{
    [SerializeField]
    private float forceStrength = 3;

    float counter;
    int time = 1;

    private void Update()
    {
        counter += Time.deltaTime;
        if(counter > time)
        {
            generate();
            counter = 0.0f;
        }
    }

    protected override void generate()
    {
        Vector3 dir = randDrop();
        VitalResource res = Instantiate(resource);
        res.transform.position = dir + transform.position;
        Rigidbody rb = res.GetComponent<Rigidbody>();
        rb.AddForce(dir * forceStrength);

    }
}
