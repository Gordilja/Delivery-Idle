using System.Collections.Generic;
using System;
using UnityEngine;

public class AdressManager : MonoBehaviour
{
    public static Action<Vector2> HomeAdress;
    public static Action<Vector2> PickUpAdress;

    private Vector2 errorVector = new Vector2(-1,-1);

    [SerializeField] private RestaurantAdressSO RestaurantAdress;
    [SerializeField] private HouseAdressSO HouseAdress;
    [SerializeField] private GameObject Coin;

    private void Start()
    {
        GameManager.ArrivedAtDestination += CheckAdress;
    }

    private void OnDisable()
    {
        GameManager.ArrivedAtDestination -= CheckAdress;
    }

    private void CheckAdress(Vector2 _position)
    {
        if (CheckList(HouseAdress.Adresses, _position) != errorVector)
        {
            HomeAdress?.Invoke(_position); 
            var _coin = Instantiate(Coin);
            _coin.transform.position = _position;
            if (GameManager.Instance.ActiveDeliveries.Count > 0)
            {
                GameManager.Instance.ActiveDeliveries.RemoveAt(0);
                GameManager.Instance.SetCurrentOrder(GameManager.Instance.ActiveDeliveries[0].RestaurantIndex, GameManager.Instance.ActiveDeliveries[0].HouseIndex);
            }
            Debug.Log("Home adress");
        }
        else 
        {
            PickUpAdress?.Invoke(_position);
            Debug.Log("Restaurant adress");
        }
    }

    private Vector2 CheckList(List<Adress> _adresses, Vector2 _position)
    {
        for (int i = 0; i < _adresses.Count; i++)
        {
            if (_adresses[i].Position == _position) 
            {
                return _adresses[i].Position;
            }
        }

        return new Vector2(-1, -1);
    }
}
