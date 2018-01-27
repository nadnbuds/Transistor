using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float interactionDistance;

    private bool canPickup = true;
    private VitalResource focusObj;

	public void Move(float horizontal, float vertical)
    {
        Vector3 newVelocity = new Vector3(
            horizontal * moveSpeed,
            gameObject.GetComponent<Rigidbody>().velocity.y, // or 0
            vertical * moveSpeed);

        this.gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
    }

    public void Action()
    {
        if (canPickup)
            pickupObj();
        else
            putDownObj();
    }

    private void pickupObj()
    {
        focusObj.transform.SetParent(transform);
        canPickup = false;
    }

    private void putDownObj()
    {
        focusObj.transform.SetParent(null);
        canPickup = true;
    }

    private void Update()
    {
        if (canPickup)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
            {
                if (hit.GetType() == typeof(VitalResource))
                {
                    focusObj = hit.transform.gameObject.GetComponent<VitalResource>();
                }
            }
        }
    }
}
