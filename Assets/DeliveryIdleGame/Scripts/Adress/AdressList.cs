using System.Collections.Generic;
using UnityEngine;

public class AdressList : MonoBehaviour
{
    [SerializeField] private RestaurantAdressSO RestaurantAdress;
    [SerializeField] private HouseAdressSO HouseAdress;
    [SerializeField] private AdressType AdressType;
    [SerializeField] private AdressPrefab Prefab;
    private List<AdressPrefab> adresses = new List<AdressPrefab>();

    private void OnEnable()
    {
        SetAdress();
    }

    private void SetAdress()
    {
        switch (AdressType) 
        {
            case AdressType.House:
                for (int _i = 0; _i < HouseAdress.Adresses.Count; _i++) 
                {
                    var _prefabObj = Instantiate(Prefab.gameObject, transform);
                    var _prefab = _prefabObj.GetComponent<AdressPrefab>();
                    _prefabObj.transform.position = HouseAdress.Adresses[_i].Position;
                    _prefab.SetText(HouseAdress.Adresses[_i].Name);
                    _prefab.SetSprite(HouseAdress.Icon);
                    adresses.Add(_prefab);
                    _prefabObj?.SetActive(false);
                }

                break;
            case AdressType.Restaurant:
                for (int _i = 0; _i < RestaurantAdress.Adresses.Count; _i++)
                {
                    var _prefabObj = Instantiate(Prefab.gameObject, transform);
                    var _prefab = _prefabObj.GetComponent<AdressPrefab>();
                    _prefabObj.transform.position = RestaurantAdress.Adresses[_i].Position;
                    _prefab.SetText(RestaurantAdress.Adresses[_i].Name);
                    _prefab.SetSprite(RestaurantAdress.Icon);
                    adresses.Add(_prefab);
                    _prefabObj?.SetActive(false);
                }
                break;
        }
    }

    public void ToggleAdress(bool _isActive, int _index) 
    {
        adresses[_index].gameObject.SetActive(_isActive);
    }
}