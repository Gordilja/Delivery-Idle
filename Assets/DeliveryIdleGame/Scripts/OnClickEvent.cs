using UnityEngine;

public class OnClickEvent : MonoBehaviour
{
    [SerializeField] private OnClickEventType EventType;
    [SerializeField] private AudioClip soundEffect;

    public int Acceleration;
    public int FillAmount;
    public int CashAmount;

    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState == GameState.Started)
        {
            OnClickAction();
        }
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
                GameManager.Instance.Car.GiveSpeedBoost(Acceleration);
                break;
            default:
                Debug.Log("No type detected");
                break;
        }

        GameManager.Instance.AudioManager.PlaySFX(soundEffect);
        gameObject.SetActive(false);
    }
}