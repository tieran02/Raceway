using UnityEngine;
using System.Collections;

[System.Serializable]
public class Waypoint
{
    [SerializeField]
    private Vector2 start;
    [SerializeField]
    private Vector2 end;
    [SerializeField]
    [Range(-1,0)]
    private float aiPathFinder = -.5f;
    [SerializeField]
    private Node node;


    public Vector2 Start
    {
        get
        {
            return start;
        }

        set
        {
            start = value;
        }
    }

    public Vector2 End
    {
        get
        {
            return end;
        }

        set
        {
            end = value;
        }
    }

    public float AIPathFinder
    {
        get
        {
            return aiPathFinder;
        }

        set
        {
            aiPathFinder = value;
        }
    }

    public Vector3 Position()
    {
        return new Vector3(AIPathCenter().x, AIPathCenter().y, 0);
    }

    public Node Node
    {
        get
        {
            return node;
        }

        set
        {
            node = value;
        }
    }

    public Vector2 AIPathCenter()
    {
        Vector2 AB = Start - End;
        return start + (aiPathFinder * AB); 
    }

    public Waypoint(Transform transform)
    {
        Vector2 right = Vector2.zero;
        Vector2 left = Vector2.zero;

        RaycastHit2D hitR = Physics2D.Raycast(transform.position, transform.right);
        Debug.DrawRay(transform.position, transform.right);
        if (hitR.collider != null || hitR.collider.tag != "collider")
            right = new Vector2(hitR.point.x, hitR.point.y);

        RaycastHit2D hitL = Physics2D.Raycast(transform.position, -transform.right);
        if (hitL.collider != null || hitL.collider.tag != "collider")
            left = new Vector2(hitL.point.x, hitL.point.y);

        Debug.DrawRay(transform.position, -transform.right, Color.green);

        start = left;
        end = right;
    }
}
