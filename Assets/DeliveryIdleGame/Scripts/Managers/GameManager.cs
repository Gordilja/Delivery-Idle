using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Action CarStop;
    public static Action GameUpdate;
    public static Action GameStart;
    public static Action<Vector2> ArrivedAtDestination;

    public UIParameters UI;

    public AdressList RestaurantAdress;
    public AdressList HouseAdress;
    public AdressManager AdressManager;
    public AudioManager AudioManager;
    public CarMovement Car;
    public GasManager GasManager;
   
    public CoinManager CoinManager;
    public StarSliderManager StarSliderPlayerManager;
    public int MaxDeliveries;
    public List<SpawnDelivery> ActiveDeliveries = new List<SpawnDelivery>();

    public GameState GameState;

    public List<Adress> AdressList = new List<Adress>();

    [SerializeField] private SpawnDelivery PopUpPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        GameUpdate?.Invoke();
    }

    public void StartGame() 
    {
#if PLATFORM_STANDALONE_WIN
        UI.GamePanel.SetActive(true);
        GameState = GameState.Started;
        CoinManager.PrepareCoins();
        GameStart?.Invoke();
#else
        UI.ClientPanel.SetActive(true);
        UI.CarIndicator.SetActive(false);
        UI.GamePanel.SetActive(false);
        SetClientText();
#endif
    }

    public void SendOrder() 
    {
        PlayerFusion.LocalPlayer.RPC_SendOrder();
    }

    public void SendRating()
    {
        PlayerFusion.LocalPlayer.RPC_SendRating(PlayerFusion.LocalPlayer.PlayerData.Player.RestaurantIndex, PlayerFusion.LocalPlayer.PlayerData.Player.HouseIndex);
    }

    public void ChangeRestaurantForDelivery() 
    {
        PlayerFusion.LocalPlayer.SetRestaurant(UI.RestaurantDropdown.value);
        Debug.Log("Changed value on dropdown");
    }

    private void SetClientText() 
    {
        UI.ClientName.text = PlayerFusion.LocalPlayer.PlayerData.Player.Name;
        UI.ClientHouse.text = AdressManager.GetHouseAdress(PlayerFusion.LocalPlayer.PlayerData.Player.HouseIndex);
    }
    
    public void GenerateOrder(int restaurantIndex, int houseIndex)
    {
        var _newPopup = Instantiate(PopUpPrefab,UI.DeliveryContent);
        _newPopup.RestaurantIndex = restaurantIndex;
        _newPopup.HouseIndex = houseIndex;
        _newPopup.SetAdressText();
    }

    public void SetCurrentOrder(int _restaurantIndex, int _houseIndex)
    {
        RestaurantAdress.ToggleAdress(true, ActiveDeliveries[0].RestaurantIndex);
        HouseAdress.ToggleAdress(true, ActiveDeliveries[0].HouseIndex);
        EnableCurrentOrder(true);
        UI.CurrentDelivery.RestaurantIndex = _restaurantIndex;
        UI.CurrentDelivery.HouseIndex = _houseIndex;
        UI.CurrentDelivery.SetAdressText();
    }

    public void EnableCurrentOrder(bool _isActive) 
    {
        UI.CurrentDelivery.gameObject.SetActive(_isActive);
    }

    public void EndGame() 
    {
        UI.GamePanel.SetActive(false);
        UI.EndPanel.SetActive(true);
        GameState = GameState.Finished;
        Car.SetSpeed(0);
    }
}