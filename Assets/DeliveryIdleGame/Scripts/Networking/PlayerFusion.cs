using Fusion;
using UnityEngine;

public class PlayerFusion : NetworkBehaviour
{
    public static PlayerFusion LocalPlayer;
    public PlayerData PlayerData;

    private void OnEnable()
    {
        if(LocalPlayer == null)
            LocalPlayer = this;

        PlayerData.LoadPlayer();
        PlayerData.Player.HouseIndex = 0;

        if(GameManager.Instance != null)
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
        // bool me done
        if (clientId != LocalPlayer.PlayerData.Player.Id) return;
        GameManager.Instance.UI.ClientPanel.LoadingPanel.SetActive(false);
        GameManager.Instance.UI.ClientPanel.RatePanel.SetActive(true);
    }
}