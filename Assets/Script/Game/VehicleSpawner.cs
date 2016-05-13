using UnityEngine;
using System.Collections;

public class VehicleSpawner : MonoBehaviour {

    public GameObject[] vehcles;
    public string path;
    public bool player;

    // Use this for initialization
    void Start () {
        path = "Prefabs\\Vehicles\\Cars";
        vehcles = Resources.LoadAll<GameObject>(path);

        spawnVehicle();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void spawnVehicle()
    {
        GameObject vehicle;
        if (player)
        {
            string vehicleName = PlayerPrefs.GetString("PlayerVehicle");
            vehicle = Instantiate(Resources.Load<GameObject>(path + "\\" + vehicleName));
            vehicle.GetComponent<Racer>().IsPlayer = true;
            VehicleColor vehicleColor = vehicle.GetComponent<VehicleColor>();
            vehicleColor.BodyColors = PlayerPrefsX.GetColorArray("rgb");
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().target = vehicle.transform;
        }
        else
        {
            int random = Random.Range(0, vehcles.Length);
            vehicle = Instantiate(vehcles[random]);
            for (int i = 0; i < vehicle.GetComponent<VehicleColor>().BodyColors.Length; i++)
            {
                vehicle.GetComponent<VehicleColor>().BodyColors[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            }
            vehicle.GetComponent<Racer>().IsPlayer = false;
        }
        vehicle.transform.position = transform.position;
        vehicle.transform.rotation = transform.rotation;
        vehicle.transform.parent = gameObject.transform.parent;
        Destroy(gameObject);
    }
}
