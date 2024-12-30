using UnityEngine;

public class Player
{
    public string Name;
    public int Coins;
    public int HouseIndex;
    public int RestaurantIndex;
    public float Rating;
    public int DeliveriesDone;

    public Player () 
    { 
        Name = "Jeff";
        Coins = 0;
        HouseIndex = 0;
        RestaurantIndex = 0;
        Rating = 0;
    }

    public void UpdatePlayerRating(float rating) 
    {
        Rating = rating;
        DeliveriesDone++;
        PlayerData.UpdateRating?.Invoke(Rating);
    }
}