using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Map : MonoBehaviour {

    public Sprite markerSprite;
    public float scale;

    int amountOfRacers;
    Image[] markers;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.instance.Racers.Count > 0 && markers == null)
        {
            amountOfRacers = GameManager.instance.Racers.Count;
            markers = new Image[amountOfRacers];

            for (int i = 0; i < amountOfRacers; i++)
            {
                if (GameManager.instance.Racers[i].IsPlayer)
                    addMarker(i, true);
                else
                    addMarker(i, false);
            }
        }
        for (int i = 0; i < amountOfRacers; i++)
        {
            Image marker = markers[i];
            marker.rectTransform.anchoredPosition = new Vector3 (GameManager.instance.Racers[i].Car.Position.x * scale, GameManager.instance.Racers[i].Car.Position.y * scale, 0);
        }
	}

    void addMarker(int index, bool player)
    {
        GameObject markerObject = new GameObject("Marker" + index);
        markerObject.transform.parent = FindObjectOfType<Map>().transform;
        markerObject.AddComponent<Image>();
        Image image = markerObject.GetComponent<Image>();
        image.sprite = markerSprite;
        image.rectTransform.sizeDelta = new Vector2(.05f,.05f);
        image.rectTransform.anchorMin  = new Vector2(0.5f,0.5f);
        image.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        image.rectTransform.localPosition = Vector3.zero;
        markers[index] = image;

        if (player)
            image.color = new Color(1,0,0,.5f);
    }

}
