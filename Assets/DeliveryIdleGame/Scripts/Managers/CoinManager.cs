using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static Action<int> AddCoins;

    public int CurrentCoins;

    public void PrepareCoins()
    {
        AddCoins += UpdateCoins;
        CurrentCoins = GameManager.Instance.PlayerController.Player.Coins;
        GameManager.Instance.UI.Coins.text = CurrentCoins.ToString();
    }

    private void OnDisable()
    {
        AddCoins -= UpdateCoins;
    }

    private void UpdateCoins(int _coins) 
    {
        GameManager.Instance.PlayerController.Player.Coins += _coins;
        CurrentCoins = GameManager.Instance.PlayerController.Player.Coins;
        GameManager.Instance.UI.Coins.text = CurrentCoins.ToString();
    }
}
