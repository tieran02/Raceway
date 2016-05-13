using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RaceFinished : MonoBehaviour {

    private HUD hud;

    private Text position;
    private Text note;

    void Awake()
    {
        hud = transform.parent.gameObject.GetComponent<HUD>();
        GameObject PanelObject = transform.GetChild(0).gameObject;
        position = PanelObject.transform.GetChild(0).GetComponent<Text>();
        note = PanelObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }

    // Use this for initialization
    void OnEnable()
    {
        hud.setHUD(false);
        SetPlayerToAI();
        UpdateStats();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void SetPlayerToAI()
    {
        CarController controller = GameManager.instance.Player.GetComponent<CarController>();
        Destroy(controller);
        GameManager.instance.Player.gameObject.AddComponent<CarAI>();
    }

    private void UpdateStats()
    {
        position.text = "You finished: " + GameManager.instance.Player.RacePosition;

        if (GameManager.instance.Player.RacePosition == 1)
        {
            note.text = "Good Job, keep it up!";
            ProfileManager.instance.ActiveProfile.Wins++;
        }
        else if (GameManager.instance.Player.RacePosition > 1 & GameManager.instance.Player.RacePosition <= 3)
        {
            note.text = "well done!";
            ProfileManager.instance.ActiveProfile.Losses++;
        }
        else if (GameManager.instance.Player.RacePosition >= 4 & GameManager.instance.Player.RacePosition <= 6)
        {
            note.text = "Better luck next time";
            ProfileManager.instance.ActiveProfile.Losses++;
        }
        else if (GameManager.instance.Player.RacePosition > 6)
        {
            note.text = "You suck!";
            ProfileManager.instance.ActiveProfile.Losses++;
        }

        ProfileManager.instance.SaveProfiles();
    }
}
