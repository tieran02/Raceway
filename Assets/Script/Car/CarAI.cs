using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour {
    Racer racer;
    public Waypoint[] path;
    int currentWaypoint = 0;


    public float frontSensorLength = 2f;
    public float frontSensorOffset;

    float Width = 0.8f;
    float Height = 0.5f;
    public bool brake = false;
    public int flag = 0;

    // Use this for initialization
    void Awake () {
        racer = GetComponent<Racer>();
        Width = GetComponent<BoxCollider2D>().size.x + .1f;
        Height = GetComponent<BoxCollider2D>().size.y + .1f;
        path = WaypointManager.instance.waypoints;
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
        RaycastHit2D FrontLeftAngle = Physics2D.Raycast(pos - transform.right * Width / 2, direction, frontSensorLength/4);
        direction = Quaternion.AngleAxis(-30.0f, transform.forward) * transform.up;
        RaycastHit2D FrontRightAngle = Physics2D.Raycast(pos + transform.right * Width / 2, direction, frontSensorLength/4 );

        //Side Raycasters
        RaycastHit2D Right = Physics2D.Raycast(pos + transform.right * Width / 2 - transform.up * Height, transform.right, .25f);
        RaycastHit2D Left = Physics2D.Raycast(pos - transform.right * Width / 2 - transform.up * Height, -transform.right, .25f);

        RaycastHit2D Right1 = Physics2D.Raycast(pos + transform.right * Width / 2 - transform.up * Height / 4, transform.right, .25f);
        RaycastHit2D Left1 = Physics2D.Raycast(pos - transform.right * Width / 2 - transform.up * Height / 4, transform.right, .25f);

        if (FrontMiddle)
        {
            if (FrontMiddle.collider.tag == "collider" && racer.Car.Speed < 1 && FrontMiddle.distance < 0.005f)
                brake = true;

            if (FrontMiddle.collider.tag == "collider" && racer.Car.Speed > 30)
                brake = true;

            else if (FrontMiddle.collider.tag == "Racer")
                if (racer.Car.Speed >= FrontMiddle.collider.GetComponent<Racer>().Car.Speed && FrontMiddle.distance < .01f)
                    brake = true;
        }
        else
            brake = false;


        if (FrontRight)
        {
            if (FrontRight.collider.tag == "collider" && racer.Car.Speed > 40)
                brake = true;

            if (FrontRight.collider.tag != "collider")
            {
                flag++;
                avoidSenstivity -= .2f;
            }
        }else if(FrontRight &&FrontMiddle)


        if (FrontLeft)
        {
            if (FrontLeft.collider.tag == "collider" && racer.Car.Speed > 40)
                brake = true;

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

            if (FrontLeftAngle.collider.tag == "collider" && FrontLeftAngle.distance <= frontSensorLength / 2)
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

            if (FrontRightAngle.collider.tag == "collider" && FrontRightAngle.distance <= frontSensorLength / 2)
            {
                flag++;
                avoidSenstivity += .2f;
            }
        }

        if (Left || Left1)
        {
                flag++;
                avoidSenstivity += .2f;
        }

        if (Right || Right1 )
        {
                flag++;
                avoidSenstivity -= .2f;
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
        Debug.DrawRay(pos - transform.right * Width / 2, direction * frontSensorLength/4);
        direction = Quaternion.AngleAxis(-30.0f, transform.forward) * transform.up;
        Debug.DrawRay(pos + transform.right * Width / 2, direction * frontSensorLength/4);

        Debug.DrawRay(pos + transform.right * Width / 2 - transform.up * Height, transform.right * .5f);
        Debug.DrawRay(pos - transform.right * Width / 2 - transform.up * Height, -transform.right * .5f);

        Debug.DrawRay(pos + transform.right * Width / 2 - transform.up * Height / 4, transform.right * .5f);
        Debug.DrawRay(pos - transform.right * Width / 2 - transform.up * Height / 4, -transform.right * .5f);
    }

}

