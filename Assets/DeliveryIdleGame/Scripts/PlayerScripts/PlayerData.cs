using System;
using System.IO;
using UnityEngine;

public static class PlayerData
{
    public static Action<float> UpdateRating;

    public static Player LoadPlayer(Player Player)
    {
        string filePath = Application.persistentDataPath + "/playerData.json";
        Player = new Player();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Player player = JsonUtility.FromJson<Player>(json);
            Player = player;
            if (Player is null)
            {
                Debug.LogWarning("Save file error, making new file.");
                Player = new Player();
            }
            Debug.Log($"Player data loaded successfully from: {filePath}");
        }
        else
        {
            Debug.LogWarning("No save file found. Creating a new Player.");
        }

        return Player;
    }

    public static void SavePlayer(Player player)
    {
        string filePath = Application.persistentDataPath + "/playerData.json";
        string json = JsonUtility.ToJson(player, true); // Pretty print for readability
        File.WriteAllText(filePath, json);
        Debug.Log($"Player data saved to: {filePath}");
    }
}