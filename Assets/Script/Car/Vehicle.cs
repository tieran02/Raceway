using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    private float speedForce = 15f;
    [SerializeField]
    private float maxSpeed = 10f;
    [SerializeField]
    private float torqueForce = -200f;
    [SerializeField]
    private float driftFactorSticky = 0.1f;
    [SerializeField]
    private float driftFactorSlippy = 0.9f;
    [SerializeField]
    private float maxStickyVelocity = 2.5f;
    [SerializeField]
    private float minSlippyVelocity = 1.5f;
    [SerializeField]
    private bool handbrake = false;

    public VehicleType vehicleType;

    public enum VehicleType
    {
        Car,
        Boat
    }

    private Vector2 position;
    private double speed;
    private float driftFactor;

    private float angularVelocity;

    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        DriftFactor = DriftFactorSticky;
	
    }

    void FixedUpdate()
    {
        if (Handbrake)
        {
            DriftFactor = .99f;
            rb.velocity = ForwardVelocity() + RightVelocity() * DriftFactor;
        }
        else
        {
            if (RightVelocity().magnitude > 2f && DriftFactor > DriftFactorSticky)
            {
                DriftFactor -= 0.001f;
            }
            else
            {
                DriftFactor = DriftFactorSticky;
                if (RightVelocity().magnitude > MaxStickyVelocity)
                {
                    DriftFactor = DriftFactorSlippy;
                }

                rb.velocity = ForwardVelocity() + RightVelocity() * DriftFactor;
            }
        }

        float tf = Mathf.Lerp(0, TorqueForce, rb.velocity.magnitude / 2);
        rb.angularVelocity = angularVelocity * tf;
        //Debug.Log(angularVelocity);
    }

    public void AddForce(Vector2 position)
    {
        rb.AddForce(position);
    }

    public void AngularVelocity(float AV)
    {
        angularVelocity = AV;
    }

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
        //Debug.Log(newSterr + "   " + transform.rotation.eulerAngles.z);
    }

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

    public double Speed
    {
        get
        {
            return rb.velocity.magnitude * 2.237 * 2;
        }
    }
}
