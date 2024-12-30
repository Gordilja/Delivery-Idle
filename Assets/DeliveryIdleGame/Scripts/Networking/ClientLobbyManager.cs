using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientLobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject HostPanel;
    [SerializeField] private GameObject ClientPanel;
    private FusionManager fusionManager;

#if !PLATFORM_STANDALONE_WIN
    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Lobby")
            fusionManager = FindFirstObjectByType<FusionManager>();

        SetClientPanel();
    }
#endif

    public void SetClientPanel() 
    {
        HostPanel.SetActive(false);
        ClientPanel.SetActive(true);
    }

    public void ExitLobby() 
    {
        fusionManager.QuitSession();
    }
}