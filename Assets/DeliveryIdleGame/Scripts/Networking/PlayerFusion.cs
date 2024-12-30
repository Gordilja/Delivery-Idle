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
    public void RPC_SendRating(int restaurantIndex, int houseIndex)
    {
        GameManager.Instance.GenerateOrder(restaurantIndex, houseIndex);
    }
}