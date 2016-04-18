using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lap : MonoBehaviour {

    Text text;
    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.Player != null)
        {
            int lap = GameManager.instance.Player.CurrentLap;
            text.text = "Lap " + lap + "/3";
        }
    }
}
