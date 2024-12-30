using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RestaurantDropdownManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown DropDown;
    [SerializeField] private RestaurantAdressSO RestaurantAdress;
    private List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

    public void FillDropdown() 
    {
        DropDown.ClearOptions();
        var list = RestaurantAdress.Adresses;

        foreach (var item in list)
        {
            options.Add(new TMP_Dropdown.OptionData(item.Name.ToString()));
        }

        DropDown.AddOptions(options);

        DropDown.value = 0;
        DropDown.RefreshShownValue();
    }
}