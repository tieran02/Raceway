using UnityEngine;
using System.Collections;

public class Mainmenu : MonoBehaviour
{
    public GameObject singleplayer;
    public GameObject multiplayer;
    public GameObject profile;
    public GameObject options; 

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowSinglePlayer()
    {
        singleplayer.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ShowMultiplayer()
    {
        multiplayer.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ShowProfile()
    {
        profile.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ShowOptions()
    {
        options.SetActive(true);
        gameObject.SetActive(false);
    }
}
