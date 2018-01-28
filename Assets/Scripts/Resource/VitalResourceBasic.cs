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
            player.HoistObj(this.gameObject);
            rb.isKinematic = true;
            col.enabled = false;
            interact = true;
        }
        else
        {
            transform.SetParent(null);
            rb.isKinematic = false;
            rb.AddForce(player.transform.forward.normalized * dropForce);
            col.enabled = true;
            interact = false;
        }
    }

    private void Awake()
    {
        base.Init();
    }
}
