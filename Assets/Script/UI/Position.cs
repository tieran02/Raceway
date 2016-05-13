using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Position : MonoBehaviour {

    Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Pos " + GameManager.instance.Player.RacePosition + "/" + GameManager.instance.Racers.Count;
    }
}
