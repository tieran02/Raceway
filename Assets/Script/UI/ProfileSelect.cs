using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProfileSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        getProfiles();
    }
	
	// Update is called once per frame
	void Update () {
	
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
        addCreateProfileButton();
    }

    private void addProfileButton(string name)
    {
        GameObject buttonObject = new GameObject(name+"Profile");
        buttonObject.transform.SetParent(transform.GetChild(1));
    }

    private void addCreateProfileButton()
    {
        UIManager.CreateButton(transform.GetChild(1), 0, 0, 0, 0, "Create Profile", createProfileForm);
        Debug.Log("test");
    }

    private void createProfileForm()
    {
        gameObject.SetActive(false);
    }

    
}
