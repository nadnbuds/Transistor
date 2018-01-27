using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType{Running, EatingContest, Test, Yoga}

[CreateAssetMenu()]
public class Event : ScriptableObject {

	public int Length;
	[SerializeField]
	private EventType eventType;
	[SerializeField]
	private List<KeyValuePair<EventType, float>> event_list;

	public void UpdateEyeMonitor(){

	}
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
