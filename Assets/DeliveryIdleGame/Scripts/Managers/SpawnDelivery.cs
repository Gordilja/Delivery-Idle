using TMPro;
using UnityEngine;

public class SpawnDelivery : MonoBehaviour
{
    [SerializeField] private RestaurantAdressSO RestaurantAdress;
    [SerializeField] private HouseAdressSO HouseAdress;

    [SerializeField] private TMP_Text AdressText;
    [SerializeField] private TMP_Text DetailsText;

    public int HouseIndex;
    public int RestaurantIndex;

    private void OnEnable()
    {
        SetAdressText();
    }

    public void AcceptDelivery()
    {
        GameManager.Instance.RestaurantAdress.ToggleAdress(true, RestaurantIndex);
        GameManager.Instance.HouseAdress.ToggleAdress(true, HouseIndex);

        GameManager.Instance.AdressList.Add(RestaurantAdress.Adresses[RestaurantIndex]);
        GameManager.Instance.AdressList.Add(HouseAdress.Adresses[HouseIndex]);

        if (GameManager.Instance.Car.CarState == CarState.Stopped)
            GameManager.Instance.Car.SetNewDestination(RestaurantAdress.Adresses[RestaurantIndex].NodeIndex);

        gameObject.SetActive(false);
    }

    public void DeclineDelivery() 
    {
        Destroy(gameObject);
    }

    public void SetAdressText() 
    {
        string _adress = $"Deliver something from {RestaurantAdress.Adresses[RestaurantIndex].Name} to {HouseAdress.Adresses[HouseIndex].Name}";
        string _details = $"Deliver the meal fresh and fast to adress!";

        AdressText.text = _adress ;
        DetailsText.text = _details ;
    }
}