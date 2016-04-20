using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleSelect : MonoBehaviour {

    public GameObject[] vehcles;
    public GameObject currentVehicle;

    private Image preview;
    private int index = 0;

	// Use this for initialization
	void Start ()
    {
        if (GameManager.instance.raceType == GameManager.RaceType.Car)
            vehcles = Resources.LoadAll<GameObject>("Prefabs\\Vehicles\\Cars");
        else if (GameManager.instance.raceType == GameManager.RaceType.Boat)
            vehcles = Resources.LoadAll<GameObject>("Prefabs\\Vehicles\\Boats");

        currentVehicle = vehcles[index];
        preview = transform.GetChild(0).GetComponent<Image>();
        preview.sprite = currentVehicle.GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
	    if(preview.sprite != currentVehicle.GetComponent<SpriteRenderer>().sprite)
            preview.sprite = currentVehicle.GetComponent<SpriteRenderer>().sprite;
    }

    public void NextVehicle()
    {
        if (index == vehcles.Length - 1)
            index = 0;
        else
            index++;

        currentVehicle = vehcles[index];
    }

    public void PreviousVehicle()
    {
        if (index == 0)
            index = vehcles.Length-1;
        else
            index--;
        currentVehicle = vehcles[index];
    }
}
