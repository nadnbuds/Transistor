using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Event))]
public class EventCustomEditor : Editor {
    
    public override void OnInspectorGUI()
    {
        Event myEvent = (Event)target;
        base.OnInspectorGUI();
    }
}
