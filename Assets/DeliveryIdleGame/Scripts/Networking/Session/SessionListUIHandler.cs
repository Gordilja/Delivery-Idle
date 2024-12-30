using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class SessionListUIHandler : MonoBehaviour
{
    public static SessionListUIHandler Instance;

    [SerializeField] TextMeshProUGUI status;
    [SerializeField] GameObject sessionItemPrefab;
    [SerializeField] VerticalLayoutGroup verticalLayoutGroup;

    private FusionManager fusionManager;

    private void Awake()
    {
        Instance = this; 
        ClearList();
    }

    public void ClearList() 
    {
        foreach (Transform item in verticalLayoutGroup.transform)
        {
            Destroy(item.gameObject);
        }
    }

    public void AddToList(SessionInfo sessionInfo) 
    {
        SessionInfoUI addedItem = Instantiate(sessionItemPrefab, verticalLayoutGroup.transform).GetComponent<SessionInfoUI>();

        addedItem.SetInfo(sessionInfo);

        addedItem.OnJoinSession += AddedItem;
    }

    private void AddedItem(SessionInfo sessionInfo) 
    {
        fusionManager.StartClient(sessionInfo.Name);
    }

    public void OnLookingSession() 
    {
        status.text = "Looking for game session";
        status.gameObject.SetActive(true);
    }
}