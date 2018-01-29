using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillProducer : Producer, Interactable
{
    private bool interact = false;

    private float counter;

    private void Awake()
    {
        SpawnInterval = 1f;
    }

    private void Update()
    {
        if (interact)
        {
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                Generate();
                counter = SpawnInterval;
            }
        }
    }

    public void ToggleInteract(PlayerController player)
    {
        if (!interact)
        {
            player.CanMove = false;
            player.transform.GetComponent<Rigidbody>().isKinematic = false;
            interact = true;
        }
        else
        {
            player.CanMove = true;
            player.transform.GetComponent<Rigidbody>().isKinematic = true;
            interact = false;
        }
    }

    public override void Generate()
    {
        Vector3 loc = RandDrop();
        VitalResource res = pooler.RetrieveCopy().GetComponent<VitalResource>();
        res.transform.position = loc + transform.position;
    }
}
