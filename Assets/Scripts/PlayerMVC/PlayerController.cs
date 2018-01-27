using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float interactionDistance;

    private bool canPickup = true;
    private Interactable focusObj;

    [HideInInspector]
    public bool CanMove = true;

	public void Move(float horizontal, float vertical)
    {
        if (CanMove)
        {
            Vector3 newVelocity = new Vector3(
            horizontal * moveSpeed,
            gameObject.GetComponent<Rigidbody>().velocity.y, // or 0
            vertical * moveSpeed);

            this.gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
        }
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
        if(focusObj != null)
        {
            focusObj.ToggleInteract(this);
            canPickup = false;
        }
    }

    private void putDownObj()
    {
        focusObj.ToggleInteract(this);
        canPickup = true;
    }

    private void Update()
    {
        if (canPickup)
        {
            focusObj = null;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
            {
                if (hit.transform.gameObject.GetComponent<Interactable>() != null)
                {
                    focusObj = hit.transform.gameObject.GetComponent<Interactable>();
                }
            }
        }
    }
}
