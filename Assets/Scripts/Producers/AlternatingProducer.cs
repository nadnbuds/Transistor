using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Web.UI;	//should give me the library for Pair but it doesn't. Why?

[System.Serializable]
struct Resource
{
    public ObjectPooler pooler;
    public float weight;
}

public class AlternatingProducer : Producer {

	[SerializeField]
	private List<Resource> resource_list;

	public override void Generate()
    {
		float sum = 0;
		foreach(Resource pair in resource_list){
			sum += pair.weight;
		}
		float random = Random.Range (0, sum);
		float prev_float = 0;
		foreach(Resource pair in resource_list){
			if (random >= prev_float && random < prev_float + pair.weight) {
                Vector3 loc = RandDrop() + transform.position;
                VitalResource res = pair.pooler.RetrieveCopy().GetComponent<VitalResource>();
                res.transform.position = loc;
                break;
            }
			prev_float = prev_float + pair.weight;
		}
	}

	// Use this for initialization
	private void Awake()
    {
        SpawnInterval = 1f;
        RegisterProducer();
	}
}
