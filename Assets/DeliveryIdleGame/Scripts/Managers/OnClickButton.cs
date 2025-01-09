using UnityEngine;

public class OnClickButton : MonoBehaviour
{
    public int index;

    public void SetOnClick()
    {
        PlayerFusion.LocalPlayer.SetRestaurant(index);
        SetColorForList();
    }

    private void SetColorForList()
    {
        for (var i = 0; i < RestaurantDropdownManager.RestaurantButtons.Count; i++)
        {
            var color = RestaurantDropdownManager.RestaurantButtons[i].image.color;

            if (i == index)
            {
                color.a = 1f;
            }
            else
            {
                color.a = 0.5f;
            }

            RestaurantDropdownManager.RestaurantButtons[i].image.color = color;
        }
    }
}