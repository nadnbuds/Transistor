using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType{Running, EatingContest, Test, Yoga}

[CreateAssetMenu()]
public class Event : ScriptableObject {

	public int Length;
	[SerializeField]
	private EventType eventType;
    public Sprite Image;
	[SerializeField]
	public List<KeyValuePair<VitalBehavior, float>> vital_list = new List<KeyValuePair<VitalBehavior, float>>();

	[SerializeField]
	public List<EventData> eventDataList = new List<EventData> ();

	public void UpdateEyeMonitor(List<KeyValuePair<VitalBehavior, float>> vital_list, EventType eventType)
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
