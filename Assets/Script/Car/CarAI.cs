using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour {
    Racer racer;
    Waypoint[] path;
    int currentWaypoint = 0;


    public float frontSensorLength = .5f;
    public float frontSensorOffset;

    float Width = 0.8f;
    float Height = 0.5f;
    public bool brake = false;
    public int flag = 0;
    // Use this for initialization
    void Start () {
        racer = GetComponent<Racer>();
        //Width = GetComponent<Renderer>().bounds.size.x;
        Height = GetComponent<Renderer>().bounds.size.y;
        path = WaypointManager.instance.waypoints;
        Debug.Log(Height);
	}

    // Update is called once per frame
    void FixedUpdate()
    {
            racer.Car.FaceTowards(path[currentWaypoint].AIPathCenter());
        MoveToWaypoint();

        RayCasting();


        if (brake)
        {
            racer.Car.AddForce(transform.up * -racer.Car.SpeedForce / 2f);
        }

    }

    void MoveToWaypoint()
    {
        if (path[currentWaypoint] != null && !brake)
        {
            racer.Car.AddForce(transform.up * racer.Car.SpeedForce);
        }
        if (racer.Checkpoints > path.Length - 1)
        {
            currentWaypoint = 0;
        }
        else
            currentWaypoint = racer.Checkpoints;

        if (path[currentWaypoint].Node.index > racer.Checkpoints)
            currentWaypoint = path[currentWaypoint].Node.index++;
    }

    void RayCasting()
    {
        flag = 0;
        float avoidSenstivity = 0f;
        //Front Raycasters
        Vector3 pos = transform.position + (transform.up * Height);
        RaycastHit2D FrontMiddle = Physics2D.Raycast(pos, transform.up, frontSensorLength);
        RaycastHit2D FrontRight = Physics2D.Raycast(pos + transform.right * Width / 2, transform.up, frontSensorLength);
        RaycastHit2D FrontLeft = Physics2D.Raycast(pos - transform.right * Width / 2, transform.up, frontSensorLength);

        //Angle Raycasters
        Vector3 direction = Quaternion.AngleAxis(30.0f, transform.forward) * transform.up;
        RaycastHit2D FrontLeftAngle = Physics2D.Raycast(pos - transform.right * Width / 2, direction, frontSensorLength);
        direction = Quaternion.AngleAxis(-30.0f, transform.forward) * transform.up;
        RaycastHit2D FrontRightAngle = Physics2D.Raycast(pos + transform.right * Width / 2, direction, frontSensorLength );

        //Side Raycasters
        RaycastHit2D Right = Physics2D.Raycast(pos + transform.right * Width / 2 - transform.up * Height / 2, transform.right * .25f);
        RaycastHit2D Left = Physics2D.Raycast(pos - transform.right * Width / 2 - transform.up * Height / 2, -transform.right * .25f);

        if (FrontMiddle)
        {
            brake = true;
        }
        else
            brake = false;


        if (FrontRight)
        {
            if (FrontRight.collider.tag != "collider")
            {
                flag++;
                avoidSenstivity -= .2f;
            }
        }


        if (FrontLeft)
        {
            if (FrontLeft.collider.tag != "collider")
            {
                flag++;
                avoidSenstivity += .2f;
            }
        }

        if (FrontLeftAngle)
        {
            if (FrontLeftAngle.collider.tag != "collider")
            {
                flag++;
                avoidSenstivity += .2f;
            }
        }

        if (FrontRightAngle)
        {
            if (FrontRightAngle.collider.tag != "collider")
            {
                flag++;
                avoidSenstivity -= .2f;
            }
        }

        if (Left)
        {
            if (Left.collider.tag != "collider")
            {
                flag++;
                avoidSenstivity += .2f;
            }
        }

        if (Right)
        {
            if (Right.collider.tag != "collider")
            {
                flag++;
                avoidSenstivity -= .2f;
            }
        }

        if (flag != 0)
            AvoidSteer(avoidSenstivity);
    }

    void AvoidSteer(float amount)
    {
        racer.Car.AngularVelocity(amount);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position + (transform.up * Height / 2);
        Debug.DrawRay(pos, transform.up * frontSensorLength);
        Debug.DrawRay(pos + transform.right * Width / 2, transform.up * frontSensorLength);
        Debug.DrawRay(pos - transform.right * Width / 2, transform.up * frontSensorLength);

        Vector3 direction = Quaternion.AngleAxis(30.0f, transform.forward) * transform.up;
        Debug.DrawRay(pos - transform.right * Width / 2, direction * frontSensorLength);
        direction = Quaternion.AngleAxis(-30.0f, transform.forward) * transform.up;
        Debug.DrawRay(pos + transform.right * Width / 2, direction * frontSensorLength);

        Debug.DrawRay(pos + transform.right * Width / 2 - transform.up * Height / 2, transform.right * .25f);
        Debug.DrawRay(pos - transform.right * Width / 2 - transform.up * Height / 2, -transform.right * .25f);
    }

}

