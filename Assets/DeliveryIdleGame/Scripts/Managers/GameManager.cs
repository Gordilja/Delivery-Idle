using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Action CarStop;

    public Canvas UI;
    public AdressList RestaurantAdress;
    public AdressList HouseAdress;
    public CarMovement Car;

    public List<Adress> AdressList = new List<Adress>();

    [SerializeField] private SpawnDelivery PopUpPrefab;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            GenerateOrder();
    }

    public void GenerateOrder() 
    {
        var _newPopup = Instantiate(PopUpPrefab, UI.transform);

        _newPopup.RestaurantIndex = RestaurantAdress.GetRandomAdress();
        _newPopup.HouseIndex = 0;//HouseAdress.GetRandomAdress();
    }
}