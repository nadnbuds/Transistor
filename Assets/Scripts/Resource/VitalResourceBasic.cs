using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalResourceBasic : VitalResource, Interactable {

    private bool interact = false;
    public override void ToggleInteract(PlayerController player)
    {
        if (!interact)
        {
            transform.SetParent(player.transform);
            GetComponent<Rigidbody>().isKinematic = true;
            interact = true;
        }
        else
        {
            transform.SetParent(null);
            GetComponent<Rigidbody>().AddForce(player.transform.forward * dropForce);
            interact = false;
        }
        
    }

    private void Awake()
    {
        base.Init();
    }
}
