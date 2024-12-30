using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void GoToLobby() 
    {
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}