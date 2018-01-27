using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DVital : MonoBehaviour
{
    void Values ()
    {

    }

    void Increment ()
    {

    }

    void Decrement ()
    {

    }

    void OnZero ()
    {

    }


}
public class Reciever : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        int count = 0;
        if (other.gameObject.GetComponent<Resource>)
        {
            //increase vital that reciever relates to
            other.GetComponent<Renderer>.enabled = false;
            ++count;
        }
    }
}
