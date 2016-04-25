using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleSelect : MonoBehaviour {

    public GameObject[] vehcles;
    public GameObject currentVehicle;
    private GameObject vehicleStats;
    private GameObject vehicleCustomise;

    private Image preview;
    private int carIndex = 0;
    public Color[] vehicleColors;

    // Use this for initialization
    void Start ()
    {
        if (GameManager.instance.raceType == GameManager.RaceType.Car)
            vehcles = Resources.LoadAll<GameObject>("Prefabs\\Vehicles\\Cars");
        else if (GameManager.instance.raceType == GameManager.RaceType.Boat)
            vehcles = Resources.LoadAll<GameObject>("Prefabs\\Vehicles\\Boats");
        preview = transform.GetChild(0).GetComponent<Image>();
        vehicleStats = transform.GetChild(1).gameObject;
        vehicleCustomise = transform.GetChild(2).gameObject;
        PreviewVehicle();
    }

    void Update()
    {
        ApplyColor();
    }
	
    public void NextVehicle()
    {
        if (carIndex == vehcles.Length - 1)
            carIndex = 0;
        else
            carIndex++;
        PreviewVehicle();
    }

    public void PreviousVehicle()
    {
        if (carIndex == 0)
            carIndex = vehcles.Length-1;
        else
            carIndex--;
        PreviewVehicle();
    }

    public void SelectCar()
    {
        Debug.Log(currentVehicle.name);
        PlayerPrefs.SetString("PlayerVehicle", vehcles[carIndex].name);
        PlayerPrefsX.SetColorArray("rgb", currentVehicle.GetComponent<VehicleColor>().BodyColors);
        Destroy(currentVehicle);
        GameManager.instance.setupGame();
        gameObject.SetActive(false);
    }

    //public void getVehicle()
    //{
    //    if (currentVehicle != vehcles[index])
    //    {
    //        for (int i = 0; i < transform.GetChild(0).transform.childCount; ++i)
    //        {
    //            if(i > 1)
    //            {
    //                Destroy(transform.GetChild(0).transform.GetChild(i).gameObject);
    //            }
    //        }
    //            currentVehicle = vehcles[index];
    //        for (int i = 0; i < currentVehicle.transform.childCount; ++i)
    //        {
    //            GameObject child = currentVehicle.transform.GetChild(i).gameObject;
    //            GameObject bodyPreviewObject = new GameObject(child.name + "Preview");
    //            bodyPreviewObject.AddComponent<RectTransform>();
    //            Image bodyPreviewImage = bodyPreviewObject.AddComponent<Image>();
    //            bodyPreviewObject.transform.parent = transform.GetChild(0);

    //            bodyPreviewImage.rectTransform.localPosition = Vector2.zero;
    //            bodyPreviewImage.sprite = child.GetComponent<SpriteRenderer>().sprite;
    //            bodyPreviewImage.rectTransform.sizeDelta = new Vector2(child.GetComponent<SpriteRenderer>().sprite.bounds.size.x, child.GetComponent<SpriteRenderer>().sprite.bounds.size.y)* 100;
    //        }
    //        getStat();
    //    }
    //}

    public void PreviewVehicle()
    {
        if (currentVehicle != null)
        {
            if (!currentVehicle.name.Contains(vehcles[carIndex].name))
            {
                Destroy(currentVehicle);
                AddVehicle();
            }
        }
        else
        {
            AddVehicle();
        }
        SetDropdown();
        Stats();
    }

    public void AddVehicle()
    {
        currentVehicle = Instantiate(vehcles[carIndex]);
        if (currentVehicle.GetComponent<RectTransform>() == null)
            currentVehicle.AddComponent<RectTransform>();
        currentVehicle.transform.SetParent(transform.GetChild(0));
        currentVehicle.transform.localPosition = new Vector3(0, 0, -10);
        currentVehicle.transform.localScale = Vector3.one * 100;

        MonoBehaviour[] comps = currentVehicle.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = false;
        }
        currentVehicle.GetComponent<Vehicle>().enabled = true;
        currentVehicle.GetComponent<VehicleColor>().enabled = true;
        vehicleColors = new Color[currentVehicle.GetComponent<VehicleColor>().BodyColors.Length];
    }

    public void Stats()
    {
        Text vehicleName = vehicleStats.transform.GetChild(0).GetComponent<Text>();
        vehicleName.text = vehcles[carIndex].name;
    }

    private void SetDropdown()
    {
        Dropdown dropdown = vehicleCustomise.transform.GetChild(0).GetComponent<Dropdown>();
        dropdown.options.Clear();
        for (int i = 0; i < currentVehicle.GetComponent<VehicleColor>().BodyColors.Length; i++)
        {
            dropdown.options.Add(new Dropdown.OptionData("Part " + (i+1)));
        }
        int TempInt = dropdown.value;
        dropdown.value = dropdown.value + 1;
        dropdown.value = TempInt;
    }

    public void ApplyColor()
    {
        float r = vehicleCustomise.transform.GetChild(1).GetComponent<Slider>().value;
        float g = vehicleCustomise.transform.GetChild(2).GetComponent<Slider>().value;
        float b = vehicleCustomise.transform.GetChild(3).GetComponent<Slider>().value;
        VehicleColor vehicleColor = currentVehicle.GetComponent<VehicleColor>();
        Dropdown dropdown = vehicleCustomise.transform.GetChild(0).GetComponent<Dropdown>();

        Color newColor = new Color(r, g, b);
        vehicleColor.BodyColors[dropdown.value] = newColor;
    }

    public void DropdownChanged()
    {
        VehicleColor vehicleColor = currentVehicle.GetComponent<VehicleColor>();
        Dropdown dropdown = vehicleCustomise.transform.GetChild(0).GetComponent<Dropdown>();

        vehicleCustomise.transform.GetChild(1).GetComponent<Slider>().value = vehicleColor.BodyColors[dropdown.value].r;
        vehicleCustomise.transform.GetChild(2).GetComponent<Slider>().value = vehicleColor.BodyColors[dropdown.value].g;
        vehicleCustomise.transform.GetChild(3).GetComponent<Slider>().value = vehicleColor.BodyColors[dropdown.value].b;
        Debug.Log(dropdown.value);
    }
}
