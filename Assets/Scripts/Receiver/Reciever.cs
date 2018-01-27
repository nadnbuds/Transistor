﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour
{
    [SerializeField]
    private Vital vital;
	
    void OnTriggerEnter(Collider other)
    {
        VitalResource resource;
        if (resource = other.gameObject.GetComponent<VitalResource>())
        {
            if (vital.ParseResource(resource))
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
