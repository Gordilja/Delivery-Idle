using UnityEngine;
using TMPro;

public class StartMenuPlayerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Gold;
    [SerializeField] private  StarSliderManager StarSliderManager;
    [SerializeField] private PlayerData PlayerData;

    private void Start()
    {
        PlayerData.LoadPlayer();
        Gold.text = PlayerData.Player.Coins.ToString();
        StarSliderManager.FillStars(PlayerData.Player.Rating);
    }
}