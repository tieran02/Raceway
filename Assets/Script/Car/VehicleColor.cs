using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleColor : MonoBehaviour {

    public Color[] BodyColors;
    private SpriteRenderer[] parts;

    // Use this for initialization
    void Awake () {
        addParts();
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].color = BodyColors[i];
        }
	}

    void addParts()
    {
        Component[] renderers = GetComponentsInChildren(typeof(SpriteRenderer));
        int amountOfParts = 0;

        //gather amount of paintable parts
        for (int i = 0; i < renderers.Length; i++)
        {
            SpriteRenderer renderer = renderers[i] as SpriteRenderer;
            if (renderer.tag == "Paintable")
            {
                amountOfParts++;
            }
        }
        parts = new SpriteRenderer[amountOfParts];
        BodyColors = new Color[amountOfParts];

        //add parts to array
        int index = 0;
        for (int i = 0; i < renderers.Length; i++)
        {
            SpriteRenderer renderer = renderers[i] as SpriteRenderer;
            if (renderer.tag == "Paintable")
            {
                parts[index] = renderer;
                Color newColor = Color.white;
                BodyColors[index] = newColor;
                index++;
            }
        }
    }
}
