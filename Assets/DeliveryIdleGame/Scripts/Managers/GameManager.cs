using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using Fusion;

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
        StarSliderPlayerManager.FillStars(PlayerFusion.LocalPlayer.Player.Rating);
#else
        UI.ClientPanel.gameObject.SetActive(true);
        UI.CarIndicator.SetActive(false);
        UI.GamePanel.SetActive(false);
        SetClientText();
        PlayerFusion.LocalPlayer.Player.HouseIndex = AdressManager.GetRandomHouseAdress();
#endif
    }

    public void SendOrder() 
    {
        PlayerFusion.LocalPlayer.SendPlayerOrder();
    }

    public void SendRating()
    {
        PlayerFusion.LocalPlayer.SendPlayerRating();
    }

    public void SetClientText() 
    {
        UI.ClientName.text = PlayerFusion.LocalPlayer.Player.Name;
        UI.ClientHouse.text = AdressManager.GetHouseAdress(PlayerFusion.LocalPlayer.Player.HouseIndex);
    }
    
    public void GenerateOrder(int restaurantIndex, int houseIndex)
    {
        var _newPopup = Instantiate(PopUp,UI.DeliveryContent);
        _newPopup.RestaurantIndex = restaurantIndex;
        _newPopup.HouseIndex = houseIndex;
        _newPopup.SetAdressText();
    }

    private WaitForSeconds msgTimer = new WaitForSeconds(3f);
    public IEnumerator OrderDecline() 
    {
        UI.ClientPanel.Message.text = "Order declined";
        yield return msgTimer;
        UI.ClientPanel.Message.text = "Waiting for order";
        UI.ClientPanel.LoadingPanel.SetActive(false);
        UI.ClientPanel.OrderPanel.SetActive(true);
        PlayerFusion.LocalPlayer.Player.HouseIndex = AdressManager.GetRandomHouseAdress();
        SetClientText();
    }

    public void GetRating(float vectorx, float vectory, float rating) 
    {
        Vector2 _position = new Vector2(vectorx, vectory);
        var _coin = Instantiate(Coin);
        _coin.transform.position = _position;
        var _starManager = _coin.GetComponent<StarSliderManager>();
        var _newRating = RatingManager.UpdateRating(rating, PlayerFusion.LocalPlayer.Player.Rating, PlayerFusion.LocalPlayer.Player.DeliveriesDone);
        PlayerFusion.LocalPlayer.Player.UpdatePlayerRating(_newRating);

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

    public void RestartGame() 
    {
        FindFirstObjectByType<FusionManager>().GetComponent<NetworkRunner>().Shutdown(true, ShutdownReason.Ok);
    }
}