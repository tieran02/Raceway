using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SinglePlayerMenu : MonoBehaviour
{
    public GameObject mainmenu;

    public void Back()
    {
        mainmenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void YachtRace()
    {
        SceneManager.LoadScene("Boat");
    }

    public void IslandTrackRace()
    {
        SceneManager.LoadScene("Island");
    }
}
