using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static Action<int> AddCoins;

    public int CurrentCoins;

    private void Start()
    {
        AddCoins += UpdateCoins;
        CurrentCoins = GameManager.Instance.PlayerController.Player.Coins;
        UpdateCoins(CurrentCoins);
    }

    private void OnDisable()
    {
        AddCoins -= UpdateCoins;
    }

    private void UpdateCoins(int _coins) 
    {
        CurrentCoins += _coins;
        GameManager.Instance.PlayerController.Player.Coins += _coins;
        GameManager.Instance.UI.Coins.text = CurrentCoins.ToString();
    }
}
