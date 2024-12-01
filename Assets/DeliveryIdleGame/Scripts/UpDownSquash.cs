using UnityEngine;
using System.Collections;

public class UpDownSquash : MonoBehaviour
{
    [Header("Movement Settings")]
    public float amplitude = 0.1f; // Height of up and down motion
    public float frequency = 4f; // Speed of up and down motion

    [Header("Squash Settings")]
    public float squashFactor = 0.75f; // Percentage of original scale (0.5 = half size)
    public float squashSpeed = 5f; // How quickly it transitions to squash/stretch

    private Vector3 initialPosition;
    private Vector3 initialScale;
    private Coroutine animationCoroutine;

    void Start()
    {
        // Store the initial position and scale of the GameObject
        initialPosition = transform.position;
        initialScale = transform.localScale;
        StartAnimation();
    }

    public void StartAnimation()
    {
        // Start the coroutine to animate the GameObject
        if (animationCoroutine == null)
        {
            animationCoroutine = StartCoroutine(AnimateUpDownSquash());
        }
    }

    public void StopAnimation()
    {
        // Stop the coroutine if it's running
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;

            // Reset the position and scale
            transform.position = initialPosition;
            transform.localScale = initialScale;
        }
    }

    private IEnumerator AnimateUpDownSquash()
    {
        while (true)
        {
            // Calculate the time-based offset for smooth looping
            float time = Time.time;
            float verticalOffset = Mathf.Sin(time * frequency) * amplitude;

            // Update position
            transform.position = initialPosition + Vector3.up * verticalOffset;

            // Calculate squash/stretch based on the vertical position
            float scaleMultiplier = Mathf.InverseLerp(-amplitude, amplitude, verticalOffset);
            float squashValue = Mathf.Lerp(squashFactor, 1f, scaleMultiplier);

            // Smoothly interpolate scale values
            Vector3 targetScale = new Vector3(
                initialScale.x * squashValue,
                initialScale.y * squashValue,
                initialScale.z
            );
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * squashSpeed);

            yield return null; // Wait for the next frame
        }
    }
}
