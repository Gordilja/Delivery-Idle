using Fusion;
using UnityEngine;

public class PlayerFusion : NetworkBehaviour, IAfterSpawned
{
    public static PlayerFusion LocalPlayer;
    public PlayerData PlayerData;

    public void StartGame() 
    {
        PlayerData.LoadPlayer();
        GameManager.Instance.StartGame();
    }

    public void SetRestaurant(int index) 
    {
        PlayerData.Player.RestaurantIndex = index;
        Debug.Log($"Restaurant Index is: {PlayerData.Player.RestaurantIndex}");
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void RPC_SendOrder(int restaurantIndex, int houseIndex)
    {
        GameManager.Instance.GenerateOrder(restaurantIndex, houseIndex);
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void RPC_SendRating(float vectorx, float vectory, float rating)
    {
        GameManager.Instance.GetRating(vectorx, vectory, rating);
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.All)]
    public void RPC_OrderFinished(int clientId)
    {
        if (clientId != LocalPlayer.PlayerData.Player.Id) return;
        GameManager.Instance.UI.ClientPanel.LoadingPanel.SetActive(false);
        GameManager.Instance.UI.ClientPanel.RatePanel.SetActive(true);
    }

    public void SendPlayerOrder() 
    {
        this.RPC_SendOrder(PlayerData.Player.RestaurantIndex, PlayerData.Player.HouseIndex);
        GameManager.Instance.UI.ClientPanel.OrderPanel.SetActive(false);
        GameManager.Instance.UI.ClientPanel.LoadingPanel.SetActive(true);
    }

    public void SendPlayerRating() 
    {
        Vector2 position = GameManager.Instance.AdressManager.GetHousePosition(PlayerData.Player.HouseIndex);
        this.RPC_SendRating(position.x, position.y, PlayerData.Player.Rating);
        GameManager.Instance.UI.ClientPanel.RatePanel.SetActive(false);
        GameManager.Instance.UI.ClientPanel.OrderPanel.SetActive(true);
    }

    public void AfterSpawned()
    {
        LocalPlayer = this;
        StartGame();
    }
}