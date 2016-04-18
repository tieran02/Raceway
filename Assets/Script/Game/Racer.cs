using UnityEngine;

public class Racer : MonoBehaviour
{
    [SerializeField]
    private string characterName;
    [SerializeField]
    private int currentLap = 1;
    [SerializeField]
    private int racePosition;
    [SerializeField]
    private bool isPlayer;
    [SerializeField]
    private int checkpoints = 0;
    private Vehicle vehcle;

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

    void Start()
    {
        Car = GetComponent<Vehicle>();
        GameManager.instance.addRacer(this);
        if (isPlayer)
        {
            if (!gameObject.GetComponent<CarController>() && GameManager.instance.raceType == GameManager.RaceType.Car)
                gameObject.AddComponent<CarController>();
            else if (!gameObject.GetComponent<BoatController>() && GameManager.instance.raceType == GameManager.RaceType.Boat)
                gameObject.AddComponent<BoatController>();
        }
        else
        {
            if (!gameObject.GetComponent<CarAI>() && GameManager.instance.raceType == GameManager.RaceType.Car)
                gameObject.AddComponent<CarAI>();
            else if (!gameObject.GetComponent<BoatAI>() && GameManager.instance.raceType == GameManager.RaceType.Boat)
                gameObject.AddComponent<BoatAI>();
        }
    }

}
