using UnityEngine;
using UnityEngine.UI;

public class GasManager : MonoBehaviour
{
    [SerializeField] private Slider GasSlider;

    private void Start()
    {
        GasSlider.maxValue = 100;
        GasSlider.value = 100;
        GasSlider.minValue = 0;
        GameManager.GameUpdate += GasUpdate;
    }

    private void OnDisable()
    {
        GameManager.GameUpdate -= GasUpdate;
    }

    private void GasUpdate()
    {
        if (GameManager.Instance.Car.CarState == CarState.Running && GasSlider.value != 0)
        {
            GasSlider.value -= GameManager.Instance.Car.Speed * Time.deltaTime;
            return;
        }
        
        if(GasSlider.value == 0)
        {
            GameManager.Instance.Car.CarState = CarState.OutOfFuel;
            GameManager.Instance.EndGame();
        }
    }

    public void FillGas(int _add) 
    {
        GasSlider.value += _add;
    }
}