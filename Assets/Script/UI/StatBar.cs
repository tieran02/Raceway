using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatBar : MonoBehaviour {
    [Range(0f, 1f)]
    public float FillAmount = 1f;

    public GameObject Bar;
    private Image BarImage;
    
    public float MaxValue { get; set; }

    public float value
    {
        set
        {
            FillAmount = Clamp(value, 0, MaxValue, 0, 1);
        }
    }

	// Use this for initialization
	void Awake ()
    {
        Bar = transform.GetChild(0).transform.GetChild(0).gameObject;
        BarImage = Bar.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        if(FillAmount != BarImage.fillAmount)
            BarImage.fillAmount = FillAmount;
    }

    private float Clamp(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public void SetValue(float value, float max)
    {
        FillAmount = Clamp(value, 0, max, 0, 1);
    }

    public void SetPercentage(float amount)
    {
        FillAmount = amount;
    }

    public void AddPercentage(float amount)
    {
        if (FillAmount + amount > 100)
            FillAmount = 100;
        else if (FillAmount - amount < 100)
            FillAmount = 0;
        else
            FillAmount += amount;
    }
}
