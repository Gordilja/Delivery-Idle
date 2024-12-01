using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnDelivery : MonoBehaviour
{
    [SerializeField] private RestaurantAdressSO RestaurantAdress;
    [SerializeField] private HouseAdressSO HouseAdress;

    [SerializeField] private TMP_Text AdressText;
    public Button AcceptButton;
    public Button DeclineButton;

    public int HouseIndex;
    public int RestaurantIndex;
    public DateTime DeliveryStartTime;
    public void AcceptDelivery()
    {
        GameManager.Instance.AdressList.Add(RestaurantAdress.Adresses[RestaurantIndex]);
        GameManager.Instance.AdressList.Add(HouseAdress.Adresses[HouseIndex]);

        if (GameManager.Instance.Car.CarState == CarState.Stopped)
            GameManager.Instance.Car.SetNewDestination(RestaurantAdress.Adresses[RestaurantIndex].NodeIndex);

        GameManager.Instance.ActiveDeliveries.Add(this);

        if(!GameManager.Instance.UI.CurrentDelivery.gameObject.activeSelf)
            GameManager.Instance.SetCurrentOrder(RestaurantIndex, HouseIndex);

        gameObject.SetActive(false);

        DeliveryStartTime = RatingManager.SaveCurrentTime();
        AdressManager.HomeAdress += TurnOffIndicators;
    }

    private void TurnOffIndicators(Vector2 _pos) 
    {
        if (AdressManager.CheckList(HouseAdress.Adresses, _pos) == AdressManager.ERROR_VECTOR) return;

       
        AdressManager.HomeAdress -= TurnOffIndicators;
    }

    public void DeclineDelivery() 
    {
        Destroy(gameObject);
    }

    public void SetAdressText()
    {
        var deliveryDistance = Vector3.Distance(RestaurantAdress.Adresses[RestaurantIndex].Position, HouseAdress.Adresses[HouseIndex].Position);
        var decimalRound = Mathf.Round(deliveryDistance * 100f) / 100f;
        string _adress = $"New Order from {RestaurantAdress.Adresses[RestaurantIndex].Name} to {HouseAdress.Adresses[HouseIndex].Name} {decimalRound} miles away";

        AdressText.text = _adress ;
    }
}