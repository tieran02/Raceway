using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateProfile : MonoBehaviour
{
    public GameObject Profiles;
    public GameObject Input;
    public void createProfile()
    {
        string name = Input.GetComponent<InputField>().text;
        ProfileManager.instance.AddProfile(name);
        Profiles.SetActive(true);
        gameObject.SetActive(false);
    }
}
