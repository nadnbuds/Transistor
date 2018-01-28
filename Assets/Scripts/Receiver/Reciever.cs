using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour
{

    [SerializeField]
    private VitalBehavior vital;
	
    void OnTriggerEnter(Collider other)
    {
        VitalResource resource;
        if (resource = other.gameObject.GetComponent<VitalResource>())
        {
            if(resource.Type == vital.GetCompatibleType() || resource.Type == ResourceType.All)
            {
                vital.Health += resource.Quantity;
                other.gameObject.SetActive(false);
            }
        }
    }
}
