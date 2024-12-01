using UnityEngine;

public class Player
{
    public int Coins;
    public string Name;
    public float Rating;
    public int DeliveriesDone;

    public Player () 
    { 
        Coins = 0;
        Name = "Player";
        Rating = 0;
    }

    public void UpdatePlayerRating(float rating) 
    {
        Rating = rating;
        DeliveriesDone++;
        PlayerController.UpdateRating?.Invoke(Rating);
    }
}