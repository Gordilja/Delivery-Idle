using Fusion;

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
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void RPC_SendOrder()
    {
        GameManager.Instance.GenerateOrder(PlayerData.Player.RestaurantIndex, PlayerData.Player.HouseIndex);
    }
}