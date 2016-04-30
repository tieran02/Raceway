using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ProfileManager : MonoBehaviour {
    [SerializeField]
    private List<Profile> profiles;

    public List<Profile> Profiles
    {
        get
        {
            return profiles;
        }

        set
        {
            profiles = value;
        }
    }
    public static ProfileManager instance = null;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this class
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        LoadProfiles();
    }

    public void AddProfile(string name)
    {
        foreach(Profile profile in profiles)
        {
            if(profile.Name == null)
            {
                profile.Name = name;
            }
        }
    }

    public void RemoveProfile(string name)
    {
        foreach (Profile profile in profiles)
        {
            if (profile.Name == name)
            {
                profiles.Remove(profile);
            }
        }
    }

    public void LoadProfiles()
    {
        if (File.Exists("BinaryMap.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            byte[] data = File.ReadAllBytes("BinaryMap.dat");
            MemoryStream ms = new MemoryStream(data);
            profiles = new List<Profile>();
            profiles = (List<Profile>)bf.Deserialize(ms);
        }
    }

    public void SaveProfiles()
    {
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, profiles);

        FileStream fs = new FileStream("Profiles.dat", FileMode.Create);
        ms.WriteTo(fs);
        fs.Close();
    }

}
