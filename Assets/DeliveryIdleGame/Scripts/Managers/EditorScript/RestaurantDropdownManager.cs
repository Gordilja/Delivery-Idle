using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class RestaurantDropdownManager : MonoBehaviour
{
    [SerializeField] private Transform ButtonParent;
    [SerializeField] private GameObject RestaurantButton;
    [SerializeField] private RestaurantAdressSO RestaurantAdress;
    public static List<Button> RestaurantButtons = new List<Button>();

    private void OnEnable()
    {
        FillButtonList();
    }

    public void FillButtonList()
    {
        ClearRestaurantList();
        var list = RestaurantAdress.Adresses;
        for (var i = 0; i < list.Count; i++)
        {
            var button = Instantiate(RestaurantButton, ButtonParent).GetComponent<Button>();
            button.gameObject.name = list[i].Name;
            button.GetComponentInChildren<TextMeshProUGUI>().text = list[i].Name.ToString();
            var onClick = button.GetComponent<OnClickButton>();
            onClick.index = i;
            button.onClick.AddListener(onClick.SetOnClick);
            RestaurantButtons.Add(button);
            if (i > 0)
            {
                var color = button.image.color;
                color.a = 0.5f;
                button.image.color = color;
            }
        }
    }

    private void ClearRestaurantList()
    {
        RestaurantButtons.Clear();
        var childCount = ButtonParent.childCount;
        for (var i = childCount - 1; i >= 0; i--) 
        {
            Destroy(ButtonParent.GetChild(i).gameObject);
        }
    }
}