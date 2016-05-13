using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Vehicle : MonoBehaviour
{
    /*
    Variables 
    */
    [SerializeField]
    private float speedForce = 15f; //The amount of force that gets added to the car per frame if acclerating
    [SerializeField]
    private float maxSpeed = 50f; // The max speed of the vehicle
    [SerializeField]
    private float torqueForce = -200f; //The amount of torque that gets apllied when turning the car
    [SerializeField]
    private float driftFactorSticky = 0.1f; //The amount of sideways velocity the car needs to stick to the road
    [SerializeField]
    private float driftFactorSlippy = 0.9f; //The amount of sideways velocity the car needs to drift
    [SerializeField]
    private float maxStickyVelocity = 2.5f; //The amount of sideways velocity required to keep traction
    [SerializeField]
    private float minSlippyVelocity = 1.5f; //The amount of sideways velocity required to stop drifting
    [SerializeField]
    private bool handbrake = false; //used to tell if the handbrake is being used
    private VehicleColor vechicleColor; // holds the vehicles color data
    private string vehicleName; //Holds the name of the vehicle

    private Vector2 position; //World position of the car
    private float speed; //Speed of the car
    private float driftFactor; //current drift factor of the car (used to tell if the car should start drifting)

    private float angularVelocity;// Angular velocity of the car

    private Rigidbody2D rb; //The pysics rigidbody used to handle all of the collision

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Assign the rigid body to the one attached to the object
        DriftFactor = DriftFactorSticky; //set driftfactor to not drift
        VechicleColor = GetComponent<VehicleColor>(); //Assign the vehicles colour from the VehicleColor class attached to the current object
        vehicleName = gameObject.name.Replace("(Clone)",""); //Assign the vehicle name and remove the clone prefix
    }

    //Fixxed update will keep looping no matter on the framerate which makes it ideal to calculate all our physic with
    void FixedUpdate()
    {
        if (Handbrake)// if the car is using its handbrake set the drift factor to .99f which makes it drift alot
        {
            DriftFactor = .99f;
            rb.velocity = ForwardVelocity() + RightVelocity() * DriftFactor;
        }
        else // if the handbrake is not true then calculate the default physics
        {
            if (RightVelocity().magnitude > 2f && DriftFactor > DriftFactorSticky) //Calculates if the car is drifting and then reduces the drift factor if the car is drifting
            {
                DriftFactor -= 0.001f;
            }
            else
            {
                DriftFactor = DriftFactorSticky; //reset the Drift factor
                if (RightVelocity().magnitude > MaxStickyVelocity)// checks if the current sidways velocity is higher than the Max sticky velcoty 
                {
                    DriftFactor = DriftFactorSlippy;// sets the drift factor to slippy and makes the car start drifting
                }

                rb.velocity = ForwardVelocity() + RightVelocity() * DriftFactor; // sets the velocity of the car to make it drift around
            }
        }

        float tf = Mathf.Lerp(0, TorqueForce, rb.velocity.magnitude / 2); //This value is the sterring modifer which depends on the cars speed
        rb.angularVelocity = angularVelocity * tf; //sets the cars angular velcity using the modifer above (this insures you can only turn whilst you're moving)
        
    }

    //This function is used by other classes to add force to the vehicle
    public void AddForce(Vector2 position)
    {
        if(Speed <  MaxSpeed)
            rb.AddForce(position);
    }

    //This function is used to set the angularVelcoity of the vehicle and enables the car to steer
    public void AngularVelocity(float AV)
    {
        angularVelocity = AV;
    }

    //This function takes in a vector and makes the car face towards it
    public void FaceTowards(Vector2 newPos)
    {
        Vector3 dir = newPos - rb.position;
        Vector2 fwdDir = rb.transform.up;
        float angDiff = Vector2.Angle(dir, fwdDir);
        Vector3 cross = Vector3.Cross(fwdDir, dir);
        Vector2 relativeVector = transform.InverseTransformPoint(newPos);
        float newSterr = (relativeVector.x / relativeVector.magnitude);
        if (cross.z > 0)
        {
            angDiff = 360 - angDiff;
        }
        if (angDiff > 1f)
        {
            AngularVelocity(newSterr);
        }
        else
        {
            AngularVelocity(0f);
        }
    }


    /*
    Getters and Setters
    */
    public Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

    public Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    public Vector2 Velocity
    {
        get
        {
            return rb.velocity;
        }

        set
        {
            rb.velocity = value;
        }
    }

    public bool Handbrake
    {
        get
        {
            return handbrake;
        }

        set
        {
            handbrake = value;
        }
    }

    public float SpeedForce
    {
        get
        {
            return speedForce;
        }

        set
        {
            speedForce = value;
        }
    }

    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }

        set
        {
            maxSpeed = value;
        }
    }

    public float TorqueForce
    {
        get
        {
            return torqueForce;
        }

        set
        {
            torqueForce = value;
        }
    }

    public float DriftFactorSticky
    {
        get
        {
            return driftFactorSticky;
        }

        set
        {
            driftFactorSticky = value;
        }
    }

    public float DriftFactorSlippy
    {
        get
        {
            return driftFactorSlippy;
        }

        set
        {
            driftFactorSlippy = value;
        }
    }

    public float MaxStickyVelocity
    {
        get
        {
            return maxStickyVelocity;
        }

        set
        {
            maxStickyVelocity = value;
        }
    }

    public float MinSlippyVelocity
    {
        get
        {
            return minSlippyVelocity;
        }

        set
        {
            minSlippyVelocity = value;
        }
    }

    public float DriftFactor
    {
        get
        {
            return driftFactor;
        }

        set
        {
            driftFactor = value;
        }
    }

    public Vector2 Position
    {
        get
        {
            return rb.position;
        }
    }

    public float Speed
    {
        get
        {
            return rb.velocity.magnitude * (float)2.237 * 2;
        }
    }

    public VehicleColor VechicleColor
    {
        get
        {
            return vechicleColor;
        }

        set
        {
            vechicleColor = value;
        }
    }

    public string VehicleName
    {
        get
        {
            return vehicleName;
        }

        set
        {
            vehicleName = value;
        }
    }
}
