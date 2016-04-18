using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour {

    Text text;
    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.Player != null)
        {
            text.text = "Speed: " + Mathf.Round((float)GameManager.instance.Player.Car.Speed);
        }
	}
}
