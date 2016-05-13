using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Profile
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int wins;
    [SerializeField]
    private int losses;
    [SerializeField]
    private double winPercentage;
    [SerializeField]
    private Vehicle favouriteCar;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public int Wins
    {
        get
        {
            return wins;
        }

        set
        {
            wins = value;
        }
    }

    public int Losses
    {
        get
        {
            return losses;
        }

        set
        {
            losses = value;
        }
    }

    public double WinPercentage
    {
        get
        {
            return winPercentage;
        }

        set
        {
            winPercentage = value;
        }
    }

    public Vehicle FavouriteCar
    {
        get
        {
            return favouriteCar;
        }

        set
        {
            favouriteCar = value;
        }
    }
}
