using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Web.UI;	//should give me the library for Pair but it doesn't. Why?

public class AlternatingProducer : Producer {

	//public VitalResource resource2;

	//public int resource_switch = 0;

	[SerializeField]
	private List<KeyValuePair<VitalResource, float>> resource_list;
	//private List<Pair<ResourceType, float>> resource_list;

	public override void Generate()
    {
		float sum = 0;
		foreach(KeyValuePair<VitalResource, float> pair in resource_list){
			sum += pair.Value;
		}
		float random = Random.Range (0, sum);
		float prev_float = 0;
		foreach(KeyValuePair<VitalResource , float> pair in resource_list){
			if (random >= prev_float && random < prev_float + pair.Value) {
				Instantiate (pair.Key, transform.position + RandDrop(), Quaternion.identity);
				return;
			}
			prev_float = prev_float + pair.Value;
		}
	}

	// Use this for initialization
	private void Awake()
    {
        SpawnInterval = 1f;
        RegisterProducer();
	}
}
