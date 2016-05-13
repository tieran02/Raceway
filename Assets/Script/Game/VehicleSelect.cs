using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleSelect : MonoBehaviour {

    public GameObject[] vehcles;
    public GameObject currentVehicleObject;
    public GameObject vehicleStats;
    public GameObject vehicleCustomise;

    private Image preview;
    private int carIndex = 0;
    public Color[] vehicleColors;

    private HUD hud;

    // Use this for initialization
    void Start ()
    {
        hud = transform.parent.gameObject.GetComponent<HUD>();
        hud.setHUD(false);
        vehcles = Resources.LoadAll<GameObject>("Prefabs\\Vehicles\\Cars");
        preview = transform.GetChild(0).GetComponent<Image>();
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
        PlayerPrefs.SetString("PlayerVehicle", vehcles[carIndex].name);
        PlayerPrefsX.SetColorArray("rgb", currentVehicleObject.GetComponent<VehicleColor>().BodyColors);
        Destroy(currentVehicleObject);
        GameManager.instance.setupGame();
        gameObject.SetActive(false);
        hud.setHUD(true);
    }

    public void PreviewVehicle()
    {
        if (currentVehicleObject != null)
        {
            if (!currentVehicleObject.name.Contains(vehcles[carIndex].name))
            {
                Destroy(currentVehicleObject);
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
        currentVehicleObject = Instantiate(vehcles[carIndex]);
        if (currentVehicleObject.GetComponent<RectTransform>() == null)
            currentVehicleObject.AddComponent<RectTransform>();
        currentVehicleObject.transform.SetParent(transform.GetChild(0));
        currentVehicleObject.transform.localPosition = new Vector3(0, 0, -10);
        currentVehicleObject.transform.localScale = Vector3.one * 200;

        MonoBehaviour[] comps = currentVehicleObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = false;
        }
        currentVehicleObject.GetComponent<Vehicle>().enabled = true;
        currentVehicleObject.GetComponent<VehicleColor>().enabled = true;
        vehicleColors = new Color[currentVehicleObject.GetComponent<VehicleColor>().BodyColors.Length];
    }

    public void Stats()
    {
        Vehicle CurrentVehicle = currentVehicleObject.GetComponent<Vehicle>();
        Text vehicleName = vehicleStats.transform.GetChild(0).GetComponent<Text>();
        StatBar speedBar = vehicleStats.transform.GetChild(1).GetComponent<StatBar>();
        StatBar accerlationBar = vehicleStats.transform.GetChild(2).GetComponent<StatBar>();
        StatBar tractionBar = vehicleStats.transform.GetChild(3).GetComponent<StatBar>();


        vehicleName.text = CurrentVehicle.VehicleName;
        speedBar.SetValue(CurrentVehicle.MaxSpeed, 100);
        accerlationBar.SetValue(CurrentVehicle.SpeedForce, 50);
        tractionBar.SetValue(CurrentVehicle.DriftFactorSticky, 1);
    }

    private void SetDropdown()
    {
        Dropdown dropdown = vehicleCustomise.transform.GetChild(0).GetComponent<Dropdown>();
        dropdown.options.Clear();
        for (int i = 0; i < currentVehicleObject.GetComponent<VehicleColor>().BodyColors.Length; i++)
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
        VehicleColor vehicleColor = currentVehicleObject.GetComponent<VehicleColor>();
        Dropdown dropdown = vehicleCustomise.transform.GetChild(0).GetComponent<Dropdown>();

        Color newColor = new Color(r, g, b);
        vehicleColor.BodyColors[dropdown.value] = newColor;
    }

    public void DropdownChanged()
    {
        VehicleColor vehicleColor = currentVehicleObject.GetComponent<VehicleColor>();
        Dropdown dropdown = vehicleCustomise.transform.GetChild(0).GetComponent<Dropdown>();

        vehicleCustomise.transform.GetChild(1).GetComponent<Slider>().value = vehicleColor.BodyColors[dropdown.value].r;
        vehicleCustomise.transform.GetChild(2).GetComponent<Slider>().value = vehicleColor.BodyColors[dropdown.value].g;
        vehicleCustomise.transform.GetChild(3).GetComponent<Slider>().value = vehicleColor.BodyColors[dropdown.value].b;
    }
}
