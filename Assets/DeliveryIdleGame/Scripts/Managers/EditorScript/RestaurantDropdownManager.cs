using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantDropdownManager : MonoBehaviour
{
    [SerializeField] private Dropdown DropDown;
    [SerializeField] private RestaurantAdressSO RestaurantAdress;
    private List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

    public void FillDropdown() 
    {
        DropDown.ClearOptions();
        var list = RestaurantAdress.Adresses;

        foreach (var item in list)
        {
            var data = new Dropdown.OptionData(item.Name);
            options.Add(data);
        }

        DropDown.AddOptions(options);

        DropDown.value = 0;
        DropDown.RefreshShownValue();
    }
}