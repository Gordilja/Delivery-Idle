using UnityEngine;

public class CameraPanControll : MonoBehaviour
{
    public float PanSpeed;

    private Camera mainCamera;
    private Vector2 screenSize;
    private float mapX, mapY;

    void Start()
    {
        SetupCameraPan();
    }

    void Update()
    {
        mainCamera.transform.position = PanVector();
    }

    private Vector3 PanVector()
    {
        Vector3 _panDirection = Vector3.zero;
        float _edgeThreshold = 0.5f;

        if (Input.mousePosition.x > screenSize.x - _edgeThreshold)
            _panDirection.x = 1;
        else if (Input.mousePosition.x < _edgeThreshold)
            _panDirection.x = -1;

        if (Input.mousePosition.y > screenSize.y - _edgeThreshold)
            _panDirection.y = 1;
        else if (Input.mousePosition.y < _edgeThreshold)
            _panDirection.y = -1;

        Vector3 _newPosition = mainCamera.transform.position + _panDirection * PanSpeed * Time.deltaTime;

        _newPosition.x = Mathf.Clamp(_newPosition.x, -mapX, mapX);
        _newPosition.y = Mathf.Clamp(_newPosition.y, -mapY, mapY);

        return _newPosition;
    }

    private void SetupCameraPan()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        mainCamera = Camera.main;
        
        // Use SpriteRenderer.size.x or .y instead of hard coded values if there are changes in resolution scaling later
        float _camWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float _camHeight = mainCamera.orthographicSize;
        mapX = 19.2f - _camWidth;
        mapY = 10.8f - _camHeight;
    }
}