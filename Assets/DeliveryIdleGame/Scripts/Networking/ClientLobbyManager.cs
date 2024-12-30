using UnityEngine;

public class ClientLobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private GameObject LobbyPanel;
    private FusionManager fusionManager;

    private void Awake()
    {
#if !PLATFORM_STANDALONE_WIN
        fusionManager = FindFirstObjectByType<FusionManager>();
        SetLobbyPanel();
#endif
    }

    public void SetLobbyPanel() 
    {
        LoadingPanel.SetActive(false);
        LobbyPanel.SetActive(true);
    }

    public void ExitLobby() 
    {
        fusionManager.QuitSession();
    }
}