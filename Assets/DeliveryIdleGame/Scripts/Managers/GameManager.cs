using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Action CarStop;
    public static Action GameUpdate;
    public static Action<Vector2> ArrivedAtDestination;

    public UIParameters UI;

    public AdressList RestaurantAdress;
    public AdressList HouseAdress;
    public CarMovement Car;
    public GasManager GasManager;
    public PlayerController PlayerController;
    public CoinManager CoinManager;
    public List<SpawnDelivery> ActiveDeliveries = new List<SpawnDelivery>();

    public GameState GameState;

    public List<Adress> AdressList = new List<Adress>();

    [SerializeField] private SpawnDelivery PopUpPrefab;


    private void Awake()
    {
        Instance = this;
        GameState = GameState.Started;
    }

    private void Update()
    {
        GameUpdate?.Invoke();

        if(Input.GetKeyDown(KeyCode.Space))
            GenerateOrder();
    }

    public void GenerateOrder()
    {
        var _newPopup = Instantiate(PopUpPrefab,UI.DeliveryContent);
        _newPopup.RestaurantIndex = RestaurantAdress.GetRandomAdress();
        _newPopup.HouseIndex = 0;//HouseAdress.GetRandomAdress();
        _newPopup.SetAdressText();
    }

    public void SetCurrentOrder(int _restaurantIndex, int _houseIndex)
    {
        EnableCurrentOrder(true);
        UI.CurrentDelivery.RestaurantIndex = _restaurantIndex;
        UI.CurrentDelivery.HouseIndex = _houseIndex;
        UI.CurrentDelivery.SetAdressText();
    }

    public void EnableCurrentOrder(bool _isActive) 
    {
        UI.CurrentDelivery.gameObject.SetActive(_isActive);
    }
}