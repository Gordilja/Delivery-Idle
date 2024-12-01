using UnityEngine;

public class ResizeWindow : MonoBehaviour
{
    [SerializeField] private Transform window;
    private Vector3 startScale;
    private Vector3 currentScale;

    private void OnEnable()
    {
        startScale = Vector3.one;
        currentScale = startScale;
    }

    public void ChangeScale() 
    {
        if (currentScale != startScale)
        {
            currentScale = startScale;
        }
        else 
        {
            currentScale = Vector3.zero;
        }

        window.localScale = currentScale;
    }
}