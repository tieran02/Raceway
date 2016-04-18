// Runtime code here
#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WaypointManager))]
public class WaypointManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WaypointManager myScript = (WaypointManager)target;
        if (GUILayout.Button("Add Waypoints"))
        {
            myScript.AddWaypoints();
        }
    }
}
#endif