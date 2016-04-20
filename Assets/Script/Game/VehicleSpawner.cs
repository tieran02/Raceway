using UnityEngine;
using System.Collections;

public class VehicleSpawner : MonoBehaviour {

    public GameObject[] vehcles;

    // Use this for initialization
    void Start () {
        if(GameManager.instance.raceType == GameManager.RaceType.Car)
            vehcles = Resources.LoadAll<GameObject>("Prefabs\\Vehicles\\Cars");
        else if (GameManager.instance.raceType == GameManager.RaceType.Boat)
            vehcles = Resources.LoadAll<GameObject>("Prefabs\\Vehicles\\Boats");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void spawnVehicle(bool player, GameObject spawner)
    {
        if (!player)
        {

        }
    }
}
