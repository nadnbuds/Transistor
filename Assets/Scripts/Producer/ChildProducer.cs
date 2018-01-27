using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildProducer : Producer {

	public override void Generate(){
		Debug.Log ("Genearting Resource...");
		Vector3 current_postion = transform.position;
		Vector3 new_position = current_postion + Vector3.down * 2;	//2 units below the producer
		//Instantiate (resource, new Vector3(0, 0, 0), Quaternion.identity);
		Instantiate<VitalResource>(resource, new_position, Quaternion.identity);
	}

	// Use this for initialization
	void Start () {
		Generate ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
