using UnityEngine;
using System.Collections;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager instance = null;

    public Waypoint[] waypoints;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        if (waypoints == null)
            AddWaypoints();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddWaypoints()
    {
        waypoints = new Waypoint[transform.childCount];
        foreach (Transform child in transform)
        {
            foreach (Component comp in child.GetComponents<Component>())
            {
                    DestroyImmediate(comp);
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform waypointTransform = transform.GetChild(i).transform;
            waypoints[i] = new Waypoint(waypointTransform);
            transform.GetChild(i).gameObject.AddComponent<Node>();
            Node node = transform.GetChild(i).gameObject.GetComponent<Node>();
            node.index = i;
            waypoints[i].Node = node;
            transform.GetChild(i).gameObject.AddComponent<BoxCollider2D>();
            BoxCollider2D boxCollider2D = transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(Vector2.Distance(waypoints[i].Start, waypoints[i].End),.5f);
            boxCollider2D.transform.position = waypoints[i].AIPathCenter();
            boxCollider2D.isTrigger = true;
        }
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(waypoints[i].Start, waypoints[i].End);
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(waypoints[i].AIPathCenter(), Vector3.one/3);

            if(i != waypoints.Length-1)
            {
                Gizmos.DrawLine(waypoints[i].AIPathCenter(), waypoints[i+1].AIPathCenter());
            }else if(i == waypoints.Length-1)
            {
                Gizmos.DrawLine(waypoints[i].AIPathCenter(), waypoints[0].AIPathCenter());
            }
        }
    }
}
