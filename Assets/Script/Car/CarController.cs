using UnityEngine;
using System.Collections;

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
        if (Input.GetButton("accelerate"))
        {
            car.AddForce(transform.up * car.SpeedForce);

        }
        if (Input.GetButton("brake"))
        {
            car.AddForce(transform.up * -car.SpeedForce / 2f);
        }

        if (Input.GetButton("handbrake"))
        {
            car.Handbrake = true;
        }else
            car.Handbrake = false;

        car.AngularVelocity(Input.GetAxis("Horizontal"));
    }
}
