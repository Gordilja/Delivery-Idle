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
        if (CheckList(HouseAdress.Adresses, _position) != ERROR_VECTOR)
        {
            HomeAdress?.Invoke(_position); 
            var _coin = Instantiate(Coin);
            _coin.transform.position = _position;
            var _starManager = _coin.GetComponent<StarSliderManager>();

            GameManager.Instance.RestaurantAdress.ToggleAdress(false, GameManager.Instance.ActiveDeliveries[0].RestaurantIndex);
            GameManager.Instance.HouseAdress.ToggleAdress(false, GameManager.Instance.ActiveDeliveries[0].HouseIndex);

            var _calculatedRating = RatingManager.CalculateRating(GameManager.Instance.ActiveDeliveries[0].DeliveryStartTime);
            var _newRating = RatingManager.UpdateRating(_calculatedRating, PlayerFusion.LocalPlayer.PlayerData.Player.Rating, PlayerFusion.LocalPlayer.PlayerData.Player.DeliveriesDone);
            PlayerFusion.LocalPlayer.PlayerData.Player.UpdatePlayerRating(_newRating);

            _starManager.FillStars(_calculatedRating);
            GameManager.Instance.StarSliderPlayerManager.FillStars(_newRating);
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
}
