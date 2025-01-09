using Fusion;
using UnityEngine;

public class PlayerFusion : NetworkBehaviour, IAfterSpawned
{
    public static PlayerFusion LocalPlayer;
    public Player Player;

    public void StartGame() 
    {
        Player = PlayerData.LoadPlayer(Player);

        GameManager.Instance.StartGame();
    }

    public void SetRestaurant(int index) 
    {
        Player.RestaurantIndex = index;
        Debug.Log($"Restaurant Index is: {Player.RestaurantIndex}");
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

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_OrderFinished(int clientId)
    {
        if (clientId != LocalPlayer.Player.Id) return;
        GameManager.Instance.UI.ClientPanel.LoadingPanel.SetActive(false);
        GameManager.Instance.UI.ClientPanel.RatePanel.SetActive(true);
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_OrderDeclined(int clientId)
    {
        if (clientId != LocalPlayer.Player.Id) 
        {
            return;
        }

        StartCoroutine(GameManager.Instance.OrderDecline());
    }

    public void SendPlayerOrder() 
    {
        this.RPC_SendOrder(Player.RestaurantIndex, Player.HouseIndex);
        GameManager.Instance.UI.ClientPanel.OrderPanel.SetActive(false);
        GameManager.Instance.UI.ClientPanel.LoadingPanel.SetActive(true);
    }

    public void SendPlayerRating() 
    {
        Vector2 position = GameManager.Instance.AdressManager.GetHousePosition(Player.HouseIndex);
        this.RPC_SendRating(position.x, position.y, Player.Rating);
        GameManager.Instance.UI.ClientPanel.RatePanel.SetActive(false);
        GameManager.Instance.UI.ClientPanel.OrderPanel.SetActive(true);
        Player.HouseIndex = GameManager.Instance.AdressManager.GetRandomHouseAdress();
        GameManager.Instance.SetClientText();
    }

    public void AfterSpawned()
    {
        if (Object.HasInputAuthority) 
        {
            LocalPlayer = this;
            StartGame();
        }

        gameObject.name = $"Player: {GetComponent<NetworkObject>().Id}";
    }
}