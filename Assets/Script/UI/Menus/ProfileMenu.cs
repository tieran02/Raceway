using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileMenu : MonoBehaviour {

    public GameObject mainmenuObject;
    private GameObject profilePanelObject;

    private Text txt_name;
    private Text txt_wins;
    private Text txt_lose;
    private Text txt_winPercentage;
    private Text txt_favouriteCar;

    void Start()
    {
        profilePanelObject = transform.GetChild(0).gameObject;
        AddStats();
    }

    void OnEnable()
    {
        UpdateStats();
    }

    private void AddStats()
    {
        txt_name = UIManager.CreateText(profilePanelObject.transform, 0, 0, 0, 0, "Name", 12).GetComponent<Text>();
        txt_wins = UIManager.CreateText(profilePanelObject.transform, 0, 0, 0, 0, "Wins", 12).GetComponent<Text>();
        txt_lose = UIManager.CreateText(profilePanelObject.transform, 0, 0, 0, 0, "Lose", 12).GetComponent<Text>();
        txt_winPercentage = UIManager.CreateText(profilePanelObject.transform, 0, 0, 0, 0, "Win percentage", 12).GetComponent<Text>();
        txt_favouriteCar = UIManager.CreateText(profilePanelObject.transform, 0, 0, 0, 0, "FavouriteCar", 12).GetComponent<Text>();
        UIManager.CreateButton(profilePanelObject.transform, 0, 0, 0, 0, "Delete Profile",12, removeProfile);
        UpdateStats();
    }

    private void UpdateStats()
    {
        txt_name.text = "Name: " + ProfileManager.instance.ActiveProfile.Name;
        txt_wins.text = "Wins: " + ProfileManager.instance.ActiveProfile.Wins;
        txt_lose.text = "Losses: " + ProfileManager.instance.ActiveProfile.Losses;
        txt_winPercentage.text = "Win Percentage: " + ProfileManager.instance.ActiveProfile.WinPercentage;
        txt_favouriteCar.text = "Favourite Car: " + ProfileManager.instance.ActiveProfile.FavouriteCar.name;
    }

    public void Back()
    {
        mainmenuObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void removeProfile()
    {
        ProfileManager.instance.RemoveProfile(ProfileManager.instance.ActiveProfile);
        SceneManager.LoadScene("ProfileSelect");
    }

}
