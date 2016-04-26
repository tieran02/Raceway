using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            ShowMenu();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShowMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }
}
