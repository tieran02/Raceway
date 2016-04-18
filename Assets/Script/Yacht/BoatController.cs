using UnityEngine;
using System.Collections;

public class BoatController : MonoBehaviour
{
    Vehicle vehcle;

    // Use this for initialization
    void Start()
    {
        vehcle = GetComponent<Vehicle>();
    }

    void Update()
    {
        if (Input.GetButton("accelerate"))
        {
            vehcle.AddForce(transform.up * vehcle.SpeedForce);

        }
        if (Input.GetButton("brake"))
        {
            vehcle.AddForce(transform.up * -vehcle.SpeedForce / 2f);
        }

        if (Input.GetButton("handbrake"))
        {
            vehcle.Handbrake = true;
        }else
            vehcle.Handbrake = false;

        vehcle.AngularVelocity(Input.GetAxis("Horizontal"));
    }
}
