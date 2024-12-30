using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static Action<int> AddCoins;

    public int CurrentCoins;

    public void PrepareCoins()
    {
        AddCoins += UpdateCoins;
        CurrentCoins = PlayerFusion.LocalPlayer.PlayerData.Player.Coins;
        GameManager.Instance.UI.Coins.text = CurrentCoins.ToString();
    }

    private void OnDisable()
    {
        AddCoins -= UpdateCoins;
    }

    private void UpdateCoins(int _coins) 
    {
        PlayerFusion.LocalPlayer.PlayerData.Player.Coins += _coins;
        CurrentCoins = PlayerFusion.LocalPlayer.PlayerData.Player.Coins;
        GameManager.Instance.UI.Coins.text = CurrentCoins.ToString();
    }
}
