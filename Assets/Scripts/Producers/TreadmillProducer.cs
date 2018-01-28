using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillProducer : Producer, Interactable
{
    private bool interact = false;

    private void Awake()
    {
        SpawnInterval = 1f;
        RegisterProducer();
    }

    public void ToggleInteract(PlayerController player)
    {
        if (!interact)
        {
            player.CanMove = false;
            interact = true;
        }
        else
        {
            player.CanMove = true;
            interact = false;
        }
    }

    public override void Generate()
    {
        Vector3 loc = RandDrop();
        VitalResource res = Instantiate(resource);
        res.transform.position = loc + transform.position;
    }
}
