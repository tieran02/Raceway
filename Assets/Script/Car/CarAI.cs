using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour {
    Racer racer; //The Racer the car is attached with

    public Waypoint[] path; //Array of waypoints the AI will use to follow
    int currentWaypoint = 0; //The current waypoint the car is on


    public float frontSensorLength = 2f; //Range that the car should dectect obsticles 

    float carWidth; //The width of the car
    float carHeight; //the height of the car
    public bool brake = false; //Used to know when the car is brakiing
    public int flag = 0;// the amount of flags the AI has(used to deteming if the car needs to avoid an obsticle)

    // Use this for initialization
    void Awake () {
        racer = GetComponent<Racer>(); // assign the racer
        carWidth = GetComponent<BoxCollider2D>().size.x + .1f; // assign the width
        carHeight = GetComponent<BoxCollider2D>().size.y + .1f; // assign the height
        path = WaypointManager.instance.waypoints; // assign the paths
    }

    // FixedUpdate is called all the time
    void FixedUpdate()
    {
        racer.Car.FaceTowards(path[currentWaypoint].AIPathCenter()); //Make the car face the next waypoint
        MoveToWaypoint(); //call the MoveToWaypoint function

        RayCasting(); // call the Raycsting function

        // if the car is braking then slow down the car
        if (brake)
        {
            racer.Car.AddForce(transform.up * -racer.Car.SpeedForce / 2f);
        }

    }

    void MoveToWaypoint()
    {
        //if the current waypoint does not equal null and the car is not braking move add force to the car
        if (path[currentWaypoint] != null && !brake)
        {
            racer.Car.AddForce(transform.up * racer.Car.SpeedForce);
        }
        //if the racers checkpoint is greater than the path array length reset it to the first checkpoint and start a new lap
        if (racer.Checkpoints > path.Length - 1)
        {
            currentWaypoint = 0;
        }
        //else continue to the next waypoint
        else
            currentWaypoint = racer.Checkpoints;

        //update the currentWaypoint
        if (path[currentWaypoint].Node.index > racer.Checkpoints)
            currentWaypoint = path[currentWaypoint].Node.index++;
    }

    void RayCasting()
    {
        flag = 0; //sets the flags to 0
        float avoidSenstivity = 0f; //This float is used to steer the car is there is an obsticle

        /*
        Sesnsors to dectect if there is an obsticle
        */
        //Front Raycasters
        Vector3 pos = transform.position + (transform.up * carHeight);
        RaycastHit2D FrontMiddle = Physics2D.Raycast(pos, transform.up, frontSensorLength);
        RaycastHit2D FrontRight = Physics2D.Raycast(pos + transform.right * carWidth / 2, transform.up, frontSensorLength);
        RaycastHit2D FrontLeft = Physics2D.Raycast(pos - transform.right * carWidth / 2, transform.up, frontSensorLength);

        //Angle Raycasters
        Vector3 direction = Quaternion.AngleAxis(30.0f, transform.forward) * transform.up;
        RaycastHit2D FrontLeftAngle = Physics2D.Raycast(pos - transform.right * carWidth / 2, direction, frontSensorLength/4);
        direction = Quaternion.AngleAxis(-30.0f, transform.forward) * transform.up;
        RaycastHit2D FrontRightAngle = Physics2D.Raycast(pos + transform.right * carWidth / 2, direction, frontSensorLength/4 );

        //Side Raycasters
        RaycastHit2D Right = Physics2D.Raycast(pos + transform.right * carWidth / 2 - transform.up * carHeight, transform.right, .25f);
        RaycastHit2D Left = Physics2D.Raycast(pos - transform.right * carWidth / 2 - transform.up * carHeight, -transform.right, .25f);

        RaycastHit2D Right1 = Physics2D.Raycast(pos + transform.right * carWidth / 2 - transform.up * carHeight / 4, transform.right, .25f);
        RaycastHit2D Left1 = Physics2D.Raycast(pos - transform.right * carWidth / 2 - transform.up * carHeight / 4, transform.right, .25f);

        //If there is an obsticle in front then brake
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

        //If there is an obsticle in front right then steer left
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

        //If there is an obsticle in front left then steer right
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

        //If there is an obsticle in front angled left then steer right
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

        //If there is an obsticle in front angled right then steer left
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

        //If there is an obsticle on the left side then steer right
        if (Left || Left1)
        {
                flag++;
                avoidSenstivity += .2f;
        }

        //If there is an obsticle on the right side then steer left
        if (Right || Right1 )
        {
                flag++;
                avoidSenstivity -= .2f;
        }

        //if there is an obsticle set the angularvelicty to avoid the object
        if (flag != 0)
            racer.Car.AngularVelocity(avoidSenstivity);
    }

    //Draw debug lines within the editor to display the sensors
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position + (transform.up * carHeight / 2);
        Debug.DrawRay(pos, transform.up * frontSensorLength);
        Debug.DrawRay(pos + transform.right * carWidth / 2, transform.up * frontSensorLength);
        Debug.DrawRay(pos - transform.right * carWidth / 2, transform.up * frontSensorLength);

        Vector3 direction = Quaternion.AngleAxis(30.0f, transform.forward) * transform.up;
        Debug.DrawRay(pos - transform.right * carWidth / 2, direction * frontSensorLength/4);
        direction = Quaternion.AngleAxis(-30.0f, transform.forward) * transform.up;
        Debug.DrawRay(pos + transform.right * carWidth / 2, direction * frontSensorLength/4);

        Debug.DrawRay(pos + transform.right * carWidth / 2 - transform.up * carHeight, transform.right * .5f);
        Debug.DrawRay(pos - transform.right * carWidth / 2 - transform.up * carHeight, -transform.right * .5f);

        Debug.DrawRay(pos + transform.right * carWidth / 2 - transform.up * carHeight / 4, transform.right * .5f);
        Debug.DrawRay(pos - transform.right * carWidth / 2 - transform.up * carHeight / 4, -transform.right * .5f);
    }

}

