using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class RatingManager : MonoBehaviour
{
    public float MaxRating = 3f;
    [SerializeField] private Sprite EmptyStar;
    [SerializeField] private Sprite Star;
    [SerializeField] private List<Image> Stars;

    private void OnEnable()
    {
        StarSprites(0);
    }

    public void SetStarRating(int rate) 
    {
        StarSprites(rate);
    }

    private void StarSprites(int index) 
    {
        //PlayerFusion.LocalPlayer.PlayerData.Player.Rating = index;
        switch (index) 
        {
            case 1:
                Stars[0].sprite = Star;
                Stars[^2].sprite = EmptyStar;
                Stars[^1].sprite = EmptyStar;
                break;
            case 2:
                Stars[0].sprite = Star;
                Stars[1].sprite = Star;
                Stars[^1].sprite = EmptyStar;
                break;  
            case 3:
                Stars[0].sprite = Star;
                Stars[1].sprite = Star;
                Stars[2].sprite = Star;
                break;
            default:
                foreach (var star in Stars) 
                {
                    star.sprite = EmptyStar;
                }
                Debug.Log("No rate");
                break;
        }
    }

    // Update the cumulative rating based on a new rating
    public static float UpdateRating(float newRating, float currentRating, int ratingCount)
    {
        if (ratingCount == 0)
        {
            // First rating directly sets the current rating
            currentRating = newRating;
        }
        else
        {
            // Weighted average for cumulative rating
            currentRating = ((currentRating * ratingCount) + newRating) / (ratingCount + 1);
        }

        Debug.Log($"Updated Rating: {currentRating} (After {ratingCount} updates)");
        return currentRating;
    }
}