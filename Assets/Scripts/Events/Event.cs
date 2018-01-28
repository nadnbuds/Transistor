using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum EventType{Running, EatingContest, Test, Yoga}

[CreateAssetMenu()]
public class Event : ScriptableObject {

	public int Length;
	//[SerializeField]
	//private EventType eventType;
    [SerializeField]
    private List<EventData> eData;
	private List<KeyValuePair<VitalBehavior, float>> event_list;

	public void UpdateEyeMonitor(List<KeyValuePair<VitalBehavior, float>> event_list, EventType eventType)
    {
        //if (event_list[].Value > 1)
        //{
        //  eyemonitor.display critical on screen
        //  
        //}
        //else if (event_list[].Value < 1 && event_list[].Value != 0)
        //{
        //  eyemonitor.display 
        //  
        //}
        //else (event_list[].Value == 0)
        //{
        //  eyemonitor.display 
        //  
        //}

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
