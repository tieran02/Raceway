using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Racer racer = other.GetComponent<Racer>();
        if (racer.Checkpoints == GameObject.FindGameObjectWithTag("WaypointManager").GetComponent<WaypointManager>().waypoints.Length)
        {
            racer.CurrentLap++;
            racer.Checkpoints = 0;
        }

    }

}
