using UnityEngine;
using System.Collections;

public class speedSound : MonoBehaviour {

    public AudioClip[] Sources;
    Vehicle vehicle;
    AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        vehicle = GetComponent<Vehicle>();
        gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.10f;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Sources != null)
        {
            if (vehicle.Speed > 0 && vehicle.Speed < 10)
            {
                audioSource.clip = Sources[0];
            }
            else if (vehicle.Speed > 10 && vehicle.Speed < 40)
            {
                audioSource.clip = Sources[1];
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
	}
}
