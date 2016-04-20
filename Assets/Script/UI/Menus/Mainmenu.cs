using UnityEngine;
using System.Collections;

public class Mainmenu : MonoBehaviour
{
    public GameObject singleplayer;
    public void Quit()
    {
        Application.Quit();
    }

    public void ShowSinglePlayer()
    {
        singleplayer.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ShowOptions()
    {

    }
}
