using UnityEngine;

public class CarController : MonoBehaviour
{
    Racer racer; //Holds the car to control

    // Use this for initialization
    void Start()
    {
        racer = GetComponent<Racer>();
    }

    //gets called every frame
    void Update()
    {
        //If the player presses accelerate 
        if (Input.GetAxis("Vertical") > 0)
        {
            // add force to the car in the direction infront of the car multiplied by the cars force amount
            racer.Car.AddForce(transform.up * racer.Car.SpeedForce);
        }
        //If the player presses brake
        if (Input.GetAxis("Vertical") < 0)
        {
            // add force to the car in the direction behind of the car multiplied by the cars force amount divided by 2
            racer.Car.AddForce(transform.up * -racer.Car.SpeedForce / 2f);
        }

        //If the played presses the key for the handbrake then enable the handbrake else reset it
        if (Input.GetButton("handbrake"))
        {
            racer.Car.Handbrake = true;
        }else
            racer.Car.Handbrake = false;

        //Add angular velocity to the car by the amount the player is steering on the horizontal axis
        racer.Car.AngularVelocity(Input.GetAxis("Horizontal"));
    }
}
