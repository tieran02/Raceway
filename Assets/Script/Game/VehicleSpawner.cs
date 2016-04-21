using UnityEngine;
using System.Collections;

public class VehicleSpawner : MonoBehaviour {

    public GameObject[] vehcles;
    public string path;
    public bool player;

    // Use this for initialization
    void Start () {
        if (GameManager.instance.raceType == GameManager.RaceType.Car)
            path = "Prefabs\\Vehicles\\Cars";
        else if (GameManager.instance.raceType == GameManager.RaceType.Boat)
            path = "Prefabs\\Vehicles\\Boats";

        vehcles = Resources.LoadAll<GameObject>(path);

        spawnVehicle();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void spawnVehicle()
    {
        if (player)
        {
            string vehicleName = PlayerPrefs.GetString("PlayerVehicle");
            Debug.Log(path + "\\" + vehicleName);
            GameObject vehicle = Instantiate(Resources.Load<GameObject>(path + "\\" + vehicleName));
            vehicle.transform.position = transform.position;
            vehicle.transform.rotation = transform.rotation;
            vehicle.GetComponent<Racer>().IsPlayer = true;
            vehicle.transform.parent = gameObject.transform.parent;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().target = vehicle.transform;
            Destroy(gameObject);
        }
        else
        {
            int random = Random.Range(0, vehcles.Length);
            GameObject vehicle = Instantiate(vehcles[random]);
            vehicle.transform.position = transform.position;
            vehicle.transform.rotation = transform.rotation;
            vehicle.GetComponent<Racer>().IsPlayer = false;
            vehicle.transform.parent = gameObject.transform.parent;
            Destroy(gameObject);
        }

    }
}
