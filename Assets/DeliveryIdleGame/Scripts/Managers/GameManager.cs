using System.Collections.Generic;
using System;
using UnityEngine;

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

    [SerializeField] private SpawnDelivery PopUp;
    [SerializeField] private GameObject Coin;

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
        UI.ClientPanel.gameObject.SetActive(true);
        UI.CarIndicator.SetActive(false);
        UI.GamePanel.SetActive(false);
        SetClientText();
#endif
    }

    public void SendOrder() 
    {
        PlayerFusion.LocalPlayer.RPC_SendOrder(PlayerFusion.LocalPlayer.PlayerData.Player.RestaurantIndex, PlayerFusion.LocalPlayer.PlayerData.Player.HouseIndex);
        UI.ClientPanel.OrderPanel.SetActive(false);
        UI.ClientPanel.LoadingPanel.SetActive(true);
    }

    public void SendRating()
    {
        Vector2 position = AdressManager.GetHousePosition(PlayerFusion.LocalPlayer.PlayerData.Player.HouseIndex);
        PlayerFusion.LocalPlayer.RPC_SendRating(position.x, position.y, PlayerFusion.LocalPlayer.PlayerData.Player.Rating);
        UI.ClientPanel.RatePanel.SetActive(false);
        UI.ClientPanel.OrderPanel.SetActive(true);
    }

    private void SetClientText() 
    {
        UI.ClientName.text = PlayerFusion.LocalPlayer.PlayerData.Player.Name;
        UI.ClientHouse.text = AdressManager.GetHouseAdress(PlayerFusion.LocalPlayer.PlayerData.Player.HouseIndex);
    }
    
    public void GenerateOrder(int restaurantIndex, int houseIndex)
    {
        var _newPopup = Instantiate(PopUp,UI.DeliveryContent);
        _newPopup.RestaurantIndex = restaurantIndex;
        _newPopup.HouseIndex = houseIndex;
        _newPopup.SetAdressText();
    }

    public void GetRating(float vectorx, float vectory, float rating) 
    {
        Vector2 _position = new Vector2(vectorx, vectory);
        var _coin = Instantiate(Coin);
        _coin.transform.position = _position;
        var _starManager = _coin.GetComponent<StarSliderManager>();
        var _newRating = RatingManager.UpdateRating(rating, PlayerFusion.LocalPlayer.PlayerData.Player.Rating, PlayerFusion.LocalPlayer.PlayerData.Player.DeliveriesDone);
        PlayerFusion.LocalPlayer.PlayerData.Player.UpdatePlayerRating(_newRating);

        _starManager.FillStars(rating);
        StarSliderPlayerManager.FillStars(_newRating);
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