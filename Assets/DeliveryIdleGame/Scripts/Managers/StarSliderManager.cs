using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StarSliderManager : MonoBehaviour
{
    [SerializeField] private List<Image> Stars;
    public bool IsPlayer;
    private void OnEnable()
    {
        if (IsPlayer)
        {
            FillStars(PlayerFusion.LocalPlayer.PlayerData.Player.Rating);
        }
    }

    public void FillStars(float rating) 
    {
        int fullStars = Mathf.FloorToInt(rating);
        float fractionalStar = rating - fullStars; 

        for (int i = 0; i < Stars.Count; i++)
        {
            if (i < fullStars)
            {
                Stars[i].fillAmount = 1f;
            }
            else if (i == fullStars)
            {
                Stars[i].fillAmount = fractionalStar;
            }
            else 
            {
                Stars[i].fillAmount = 0f;
            }
        }
    }
}