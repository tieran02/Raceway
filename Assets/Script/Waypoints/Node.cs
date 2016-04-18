using UnityEngine;
using System.Collections;
[System.Serializable]
public class Node : MonoBehaviour {
    public int index;
    public int speed;
    public bool curve;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Racer racer = other.GetComponent<Racer>();
        if (racer.Checkpoints > GameObject.FindGameObjectWithTag("WaypointManager").GetComponent<WaypointManager>().waypoints.Length)
            racer.Checkpoints = 0;
        else if(racer.Checkpoints == index)
            racer.Checkpoints++;
    }
}
