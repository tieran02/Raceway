using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ProfileSelect : MonoBehaviour {

    public GameObject createProfile;

	// Use this for initialization
	void Start () {
        Transform parent = transform.GetChild(1);

        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
        getProfiles();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        Transform parent = transform.GetChild(1);

        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
        getProfiles();
    }

    public void getProfiles()
    {
        if (ProfileManager.instance.Profiles != null)
        {
            foreach (Profile profile in ProfileManager.instance.Profiles)
            {
                if (profile.Name != null)
                    addProfileButton(profile.Name);
            }
        }
        if(ProfileManager.instance.Profiles.Count < 3)
            addCreateProfileButton();
    }

    private void addProfileButton(string name)
    {
        UIManager.CreateButton(transform.GetChild(1), 0, 0, 0, 0, name,24, loadProfile);
    }

    private void addCreateProfileButton()
    {
        UIManager.CreateButton(transform.GetChild(1), 0, 0, 0, 0, "Create Profile",24, createProfileForm);
        Debug.Log("test");
    }

    private void createProfileForm()
    {
        createProfile.SetActive(true);
        gameObject.SetActive(false);
    }

    private void loadProfile()
    {
        ProfileManager.instance.ActiveProfile = ProfileManager.instance.getProfile(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
        SceneManager.LoadScene("Menu");
    }
}
