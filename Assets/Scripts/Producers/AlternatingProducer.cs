using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingProducer : Producer {

	public VitalResource resource2;
	public int resource_switch = 0;

	protected override void generate(){
		if (resource_switch == 0) {
			Instantiate (resource, randDrop (), Quaternion.identity);
			resource_switch = 1;		
		} else {
			Instantiate (resource2, randDrop (), Quaternion.identity);
			resource_switch = 0;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//generate ();
	}
}
