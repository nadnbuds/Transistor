using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillProducer : Producer, Interactable {

    private bool interact = false;

    float counter;
    int time = 1;

    private void Update()
    {
        if (interact)
        {
            counter += Time.deltaTime;
            if (counter > time)
            {
                generate();
                counter = 0.0f;
            }
        }
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

    protected override void generate()
    {
        Vector3 loc = randDrop();
        VitalResource res = Instantiate(resource);
        res.transform.position = loc + transform.position;
    }
}
