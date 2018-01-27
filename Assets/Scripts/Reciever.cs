﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour
{
    [SerializeField]
    private Vital vital;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        VitalResource resource;
        if (resource = other.gameObject.GetComponent<VitalResource>())
        {
            //increase vital that reciever relates to
            other.gameObject.SetActive(false);
            vital.Increment(resource.Value);
        }
    }
}
