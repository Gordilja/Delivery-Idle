using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Action CarStop;
    public static Action GameUpdate;
    public static Action<Vector2> ArrivedAtDestination;

    public UIParameters UI;

    public AdressList RestaurantAdress;
    public AdressList HouseAdress;
    public AudioManager AudioManager;
    public CarMovement Car;
    public GasManager GasManager;
    public PlayerController PlayerController;
    public CoinManager CoinManager;
    public StarSliderManager StarSliderPlayerManager;
    public int MaxDeliveries;
    public List<SpawnDelivery> ActiveDeliveries = new List<SpawnDelivery>();

    public GameState GameState;

    public List<Adress> AdressList = new List<Adress>();

    [SerializeField] private SpawnDelivery PopUpPrefab;
    private WaitForSeconds nextOrderAutomated = new WaitForSeconds(10f);

    private void Awake()
    {
        Instance = this;
        PlayerController.LoadPlayer();
    }

    private void Update()
    {
        GameUpdate?.Invoke();
    }

    public void StartGame() 
    {   
        CoinManager.PrepareCoins();
        UI.StartPanel.SetActive(false);
        UI.GamePanel.SetActive(true);
        GameState = GameState.Started;
        StartCoroutine(AutomatedOrders());
    }

    public void RestartGame() 
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private IEnumerator AutomatedOrders() 
    {
        while (GameState != GameState.Finished) 
        {
            yield return nextOrderAutomated;

            if(ActiveDeliveries.Count < MaxDeliveries)
                GenerateOrder();
        }
    }

    public void GenerateOrder()
    {
        var _newPopup = Instantiate(PopUpPrefab,UI.DeliveryContent);
        _newPopup.RestaurantIndex = RestaurantAdress.GetRandomAdress();
        _newPopup.HouseIndex = HouseAdress.GetRandomAdress();
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

    public void QuitGame() 
    {
        Application.Quit();
    }
}