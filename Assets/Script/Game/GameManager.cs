using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public List<Racer> Racers;
    public Racer Player;
    public Racer[] racerOrder;

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

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    void InitGame() {
        Racers = new List<Racer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Player == null)
        {
            Player = getPlayer();
        }

        foreach (Racer racer in Racers)
        {
            racer.RacePosition = racer.GetCarPosition(Racers.ToArray()) ;
        }
    }

    public void addRacer(Racer racer)
    {
        Racers.Add(racer);
    }

    private Racer getPlayer()
    {
        foreach (Racer racer in Racers)
        {
            if (racer.IsPlayer)
                return racer;
        }
        return null;
    }

    public void setupGame()
    {
        GameObject[] spawners;
        spawners = GameObject.FindGameObjectsWithTag("Spawners");

        for (int i = 0; i < spawners.Length; i++)
        {
            if(i == 0)
            {
                //setup player
                spawners[i].AddComponent<VehicleSpawner>();
                VehicleSpawner vehicleSpawner = spawners[i].GetComponent<VehicleSpawner>();
                vehicleSpawner.player = true;
            }
            else
            {
                //setup AI
                spawners[i].AddComponent<VehicleSpawner>();
                VehicleSpawner vehicleSpawner = spawners[i].GetComponent<VehicleSpawner>();
                vehicleSpawner.player = false;
            }
        }
    }
}
