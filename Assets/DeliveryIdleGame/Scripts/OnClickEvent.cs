using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private OnClickEventType EventType;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData == null) return;

       
        Debug.Log("Clicked event");
    }

    private void OnClickAction() 
    {
        switch (EventType)
        {
            case OnClickEventType.Oil:
                // Add oil
                break;
            case OnClickEventType.Coin:
                // Add cash
                break;
            case OnClickEventType.Accelerate:
                GameManager.Instance.Car.SetSpeed();
                gameObject.SetActive(false);
                break;
            default:
                Debug.Log("No type detected");
                break;
        }
    }
}
