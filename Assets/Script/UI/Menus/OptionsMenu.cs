using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour
{

    public GameObject mainmenu;

    public void Back()
    {
        mainmenu.SetActive(true);
        gameObject.SetActive(false);
    }
}