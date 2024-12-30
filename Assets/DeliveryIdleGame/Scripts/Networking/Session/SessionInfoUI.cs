using Fusion;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sessionName;
    [SerializeField] private TextMeshProUGUI playerCount;
    [SerializeField] private Button joinButton;

    private SessionInfo sessionInfo;

    public event Action<SessionInfo> OnJoinSession;

    public void SetInfo(SessionInfo sessionInfo) 
    {
        this.sessionInfo = sessionInfo;

        sessionName.text = sessionInfo.Name;
        playerCount.text = $"{sessionInfo.PlayerCount.ToString()}/{sessionInfo.MaxPlayers.ToString()}";

        bool isJoinButtonActive = true;

        if(sessionInfo.PlayerCount >= sessionInfo.MaxPlayers)
            isJoinButtonActive = false;

        joinButton.gameObject.SetActive(isJoinButtonActive);
    }

    public void OnClick() 
    {
        OnJoinSession?.Invoke(sessionInfo);
    }
}