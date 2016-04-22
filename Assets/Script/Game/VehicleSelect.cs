using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleSelect : MonoBehaviour {

    public GameObject[] vehcles;
    public GameObject currentVehicle;
    private GameObject vehicleStats;

    private Image preview;
    private int index = 0;

	// Use this for initialization
	void Start ()
    {
        if (GameManager.instance.raceType == GameManager.RaceType.Car)
            vehcles = Resources.LoadAll<GameObject>("Prefabs\\Vehicles\\Cars");
        else if (GameManager.instance.raceType == GameManager.RaceType.Boat)
            vehcles = Resources.LoadAll<GameObject>("Prefabs\\Vehicles\\Boats");
        preview = transform.GetChild(0).GetComponent<Image>();
        vehicleStats = transform.GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        getVehicle();
    }

    public void NextVehicle()
    {
        if (index == vehcles.Length - 1)
            index = 0;
        else
            index++;
    }

    public void PreviousVehicle()
    {
        if (index == 0)
            index = vehcles.Length-1;
        else
            index--;
    }

    public void SelectCar()
    {
        Debug.Log(currentVehicle.name);
        PlayerPrefs.SetString("PlayerVehicle", currentVehicle.name);
        GameManager.instance.setupGame();
        gameObject.SetActive(false);
    }

    public void getVehicle()
    {
        if (currentVehicle != vehcles[index])
        {
            for (int i = 0; i < transform.GetChild(0).transform.childCount; ++i)
            {
                if(i > 1)
                {
                    Destroy(transform.GetChild(0).transform.GetChild(i).gameObject);
                }
            }
                currentVehicle = vehcles[index];
            for (int i = 0; i < currentVehicle.transform.childCount; ++i)
            {
                GameObject child = currentVehicle.transform.GetChild(i).gameObject;
                GameObject bodyPreviewObject = new GameObject(child.name + "Preview");
                bodyPreviewObject.AddComponent<RectTransform>();
                Image bodyPreviewImage = bodyPreviewObject.AddComponent<Image>();
                bodyPreviewObject.transform.parent = transform.GetChild(0);

                bodyPreviewImage.rectTransform.localPosition = Vector2.zero;
                bodyPreviewImage.sprite = child.GetComponent<SpriteRenderer>().sprite;
                bodyPreviewImage.rectTransform.sizeDelta = new Vector2(child.GetComponent<SpriteRenderer>().sprite.bounds.size.x, child.GetComponent<SpriteRenderer>().sprite.bounds.size.y)* 100;
            }
            getStat();
        }
    }

    public void getStat()
    {
        Text vehicleName = vehicleStats.transform.GetChild(0).GetComponent<Text>();
        vehicleName.text = currentVehicle.name;
    }
}
