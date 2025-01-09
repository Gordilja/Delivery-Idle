using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FusionManager : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef _playerPrefab;

    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
    private NetworkRunner _runner;

    private void Awake()
    {
        _runner = GetComponent<NetworkRunner>();
#if PLATFORM_STANDALONE_WIN
        StartNetworking(GameMode.Host, "Test Game");
#else
        JoinLobby();
#endif
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, Vector3.zero, Quaternion.identity, player);
            // Keep track of the player avatars for easy access
            _spawnedCharacters.Add(player, networkPlayerObject);
        }
    }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        //end game
        if (player.IsMasterClient)
        {
            runner.Shutdown(true, ShutdownReason.GameClosed);
        }
        else 
        {
            runner.Despawn(runner.GetPlayerObject(player));
        }
    }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
        Destroy(runner.gameObject);
    }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) 
    {
        runner.Shutdown(true, ShutdownReason.GameNotFound);
    }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        runner.Shutdown(true, ShutdownReason.Error);
    }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        //lobby
        if (SessionListUIHandler.Instance == null)
            return;

        if (sessionList.Count == 0)
        {
            Debug.Log("No sessions in lobby");

            SessionListUIHandler.Instance.OnLookingSession();
        }
        else 
        {
            SessionListUIHandler.Instance.ClearList();

            foreach (SessionInfo sessionInfo in sessionList) 
            {
                SessionListUIHandler.Instance.AddToList(sessionInfo);

                Debug.Log("Found and added session");
            }
        }

    }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }

    public void StartClient(string sessionName)
    {
        StartNetworking(GameMode.Client, sessionName);
    }

    private async Task JoinLobby() 
    {
        var result = await _runner.JoinSessionLobby(SessionLobby.Custom, "DeliveryID");

        if (!result.Ok)
        {
            Debug.Log("Unable to join lobby");
        }
        else 
        {
            Debug.Log("JoinedLobby");
        }
    }

    async void StartNetworking(GameMode mode, string sessionName)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner.ProvideInput = true;

        // Create the NetworkSceneInfo from the current scene
        var scene = SceneRef.FromIndex(2);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            Scene = scene,
            SessionName = sessionName,
            CustomLobbyName = "DeliveryID",
            SceneManager = gameObject.GetComponent<NetworkSceneManagerDefault>()
        });
    }

    public void QuitSession() 
    {
        _runner.Shutdown(true, ShutdownReason.Ok);
    }
}