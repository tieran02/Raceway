using UnityEngine;
using System.Collections;

public class Skid : MonoBehaviour {

    Vehicle car;
    TrailRenderer skid;
    public Material skidMaterial;
    public bool drifting = false;
    // Use this for initialization
    void Start () {
        car = gameObject.GetComponentInParent<Vehicle>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(car.DriftFactor >= car.DriftFactorSlippy && !drifting)
        {
            addSkid();
            skidMaterial.SetColor("_TintColor", new Color(1f, 1f, 1f, 0f));
        }
        else if(car.DriftFactor <= car.DriftFactorSlippy && drifting)
        {
            skidMaterial.SetColor("_TintColor", new Color(1f,1f,1f,1f));
        }
	}

    void addSkid()
    {
        skid = gameObject.AddComponent<TrailRenderer>();
        skid.time = 10;
        skid.startWidth = 0.06f;
        skid.endWidth = 0.06f;
        skid.material = skidMaterial;
        drifting = true;
    }

    void removeSkid()
    {
        Destroy(skid);
        drifting = false;
    }
}
