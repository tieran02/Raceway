using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CarController : MonoBehaviour
{
    Vehicle car;

    // Use this for initialization
    void Start()
    {
        car = GetComponent<Vehicle>();
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
        {
            car.AddForce(transform.up * car.SpeedForce);

        }
        if (CrossPlatformInputManager.GetAxis("Vertical") < 0)
        {
            car.AddForce(transform.up * -car.SpeedForce / 2f);
        }

        if (Input.GetButton("handbrake"))
        {
            car.Handbrake = true;
        }else
            car.Handbrake = false;

        car.AngularVelocity(CrossPlatformInputManager.GetAxis("Horizontal"));
    }
}
