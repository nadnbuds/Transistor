using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class UserInput : MonoBehaviour {

    [SerializeField]
    private PlayerController player;

    private float horizontal, vertical;

    private bool action;

	private void Update () {
        if (Input.GetButtonDown("Space"))
            action = true;
        else if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        player.Move(horizontal, vertical);
        if (action)
        {
            player.Action();
            action = false;
        }
        
    }
}
