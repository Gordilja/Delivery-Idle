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
    }

    private void Update()
    {
        if(GameManager.Instance.Car.CarState == CarState.Running)
            GasSlider.value -= 5 * Time.deltaTime;
    }
}
