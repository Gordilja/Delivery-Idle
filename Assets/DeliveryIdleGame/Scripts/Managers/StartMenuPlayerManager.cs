using UnityEngine;
using TMPro;

public class StartMenuPlayerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Gold;
    [SerializeField] private TextMeshProUGUI DeliveriesNum;
    [SerializeField] private  StarSliderManager StarSliderManager;
    public Player Player;

    private void OnEnable()
    {
        Player = PlayerData.LoadPlayer(Player);
    }

    private void Start()
    {
        Gold.text = Player.Coins.ToString();
        DeliveriesNum.text = $"Deliveries done: {Player.DeliveriesDone}";
        StarSliderManager.FillStars(Player.Rating);
    }
}