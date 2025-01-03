using System;
using System.IO;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Player Player;
    public static Action<float> UpdateRating;
    private string filePath = Path.Combine(Application.dataPath, "playerData.json");

    public void LoadPlayer()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Player player = JsonUtility.FromJson<Player>(json);
            Player = player;
            Debug.Log("Player data loaded successfully.");
        }
        else
        {
            Debug.LogWarning("No save file found. Creating a new Player.");
            Player =  new Player(); // Return a new Player if no save exists
        }
    }

    public void SavePlayer(Player player)
    {
        string json = JsonUtility.ToJson(player, true); // Pretty print for readability
        File.WriteAllText(filePath, json);
        Debug.Log($"Player data saved to: {filePath}");
    }

    private void OnDisable()
    {
        SavePlayer(Player);
    }
}