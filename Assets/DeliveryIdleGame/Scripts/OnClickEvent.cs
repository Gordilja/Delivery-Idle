using UnityEngine;

public class OnClickEvent : MonoBehaviour
{
    [SerializeField] private OnClickEventType EventType;

    public int Acceleration;
    public int FillAmount;
    public int CashAmount;

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
                GameManager.Instance.GasManager.FillGas(FillAmount);
                break;
            case OnClickEventType.Coin:
                CoinManager.AddCoins?.Invoke(CashAmount);
                break;
            case OnClickEventType.Accelerate:
                GameManager.Instance.Car.SetSpeed(Acceleration);
                break;
            default:
                Debug.Log("No type detected");
                break;
        }

        gameObject.SetActive(false);
    }
}