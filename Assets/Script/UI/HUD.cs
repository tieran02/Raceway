using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    public GameObject HUDParent;
    bool isVisable = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setHUD(bool visable)
    {
        HUDParent.SetActive(visable);
    }
}
