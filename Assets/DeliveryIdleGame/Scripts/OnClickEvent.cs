using UnityEngine;

public class OnClickEvent : MonoBehaviour
{
    [SerializeField] private OnClickEventType EventType;

    public int Acceleration;

    private void OnMouseDown()
    {
        OnClickAction();
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
                GameManager.Instance.Car.SetSpeed(Acceleration);
                gameObject.SetActive(false);
                break;
            default:
                Debug.Log("No type detected");
                break;
        }
    }
}