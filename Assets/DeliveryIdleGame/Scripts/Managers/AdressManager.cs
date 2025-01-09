using System.Collections.Generic;
using System;
using UnityEngine;

public class AdressManager : MonoBehaviour
{
    public static Action<Vector2> HomeAdress;
    public static Action<Vector2> PickUpAdress;

    public static Vector2 ERROR_VECTOR = new Vector2(-1,-1);

    [SerializeField] private RestaurantAdressSO RestaurantAdress;
    [SerializeField] private HouseAdressSO HouseAdress;

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
        if (CheckList(HouseAdress.Adresses, _position) != ERROR_VECTOR)
        {
            HomeAdress?.Invoke(_position);
            PlayerFusion.LocalPlayer.RPC_OrderFinished(GameManager.Instance.ActiveDeliveries[0].ClientId);

            GameManager.Instance.RestaurantAdress.ToggleAdress(false, GameManager.Instance.ActiveDeliveries[0].RestaurantIndex);
            GameManager.Instance.HouseAdress.ToggleAdress(false, GameManager.Instance.ActiveDeliveries[0].HouseIndex);
           
            GameManager.Instance.ActiveDeliveries.RemoveAt(0);

            if (GameManager.Instance.ActiveDeliveries.Count > 0)
            {
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

    public static Vector2 CheckList(List<Adress> _adresses, Vector2 _position)
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

    public List<Adress> GetRestaurants() 
    {
        return RestaurantAdress.Adresses;
    }

    public string GetHouseAdress(int index) 
    {
        return HouseAdress.Adresses[index].Name;
    }

    public Vector2 GetHousePosition(int index) 
    {
        return HouseAdress.Adresses[index].Position;
    }

    public int GetRandomHouseAdress()
    {
        int index = UnityEngine.Random.Range(0, HouseAdress.Adresses.Count);
        return index;
    }
}
