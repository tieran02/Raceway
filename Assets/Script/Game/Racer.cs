using UnityEngine;

public class Racer : MonoBehaviour
{
    /*
    Variables 
    */
    [SerializeField]
    private string characterName; //The racers name
    [SerializeField]
    private int currentLap = 1; //The current lap the racer is on
    [SerializeField]
    private int racePosition; //the postition the racer is compared to the other racers
    [SerializeField]
    private bool isPlayer; //used to tell if the racer is the player
    [SerializeField]
    private int checkpoints = 0; //checkpoints the racer has passed through
    private Vehicle vehcle; // The racers vehicle

    public float GetDistance()
    {
        return (transform.position - WaypointManager.instance.waypoints[Mathf.Clamp(checkpoints-1,0, WaypointManager.instance.waypoints.Length)].Position()).magnitude + checkpoints * 100 + currentLap * 10000;
    }

    public int GetCarPosition(Racer[] allRacers)
    {
        float distance = GetDistance();
        int position = 1;
        foreach (Racer racer in allRacers)
        {
            if (racer.GetDistance() > distance)
                position++;
        }
        return position;
    }

    //Getter and setters for the private variables to make them accesibe by other classes
    public string CharacterName
    {
        get
        {
            return characterName;
        }

        set
        {
            characterName = value;
        }
    }
    public int CurrentLap
    {
        get
        {
            return currentLap;
        }

        set
        {
            currentLap = value;
        }
    }
    public int RacePosition
    {
        get
        {
            return racePosition;
        }

        set
        {
            racePosition = value;
        }
    }
    public bool IsPlayer
    {
        get
        {
            return isPlayer;
        }

        set
        {
            isPlayer = value;
        }
    }

    public Vehicle Car
    {
        get
        {
            return vehcle;
        }

        set
        {
            vehcle = value;
        }
    }

    public int Checkpoints
    {
        get
        {
            return checkpoints;
        }

        set
        {
            checkpoints = value;
        }
    }

    //This function gets called when the game scene loads
    void Start()
    {
        vehcle = GetComponent<Vehicle>(); //assign the vehicle variable
        GameManager.instance.addRacer(this); //add this new racer to the list of racers to be used by the gamemanager class
        if (isPlayer) // if the racer is the player add the Carcontoller otherwise make it an AI and add the AI script to the car
        {
                gameObject.AddComponent<CarController>();
        }
        else
        {
                gameObject.AddComponent<CarAI>();
        }
    }

}
