using UnityEngine;

public class CameraPanControll : MonoBehaviour
{
    public float PanSpeed;

    private Camera mainCamera;
    private Vector2 screenSize;
    private float mapX, mapY;

    private float zoom;
    private float zoomMultiplier = 5f;
    private float maxZoom = 9f;
    private float minZoom = 5f;
    private float velocity = 0f;
    private float smoothTime = 0.01f;

    void Awake()
    {
        GameManager.GameStart += StartGame;
        SetupCameraPan();
        zoom = mainCamera.orthographicSize;
    }

    private void StartGame() 
    {
        GameManager.GameUpdate += CameraPan;
        GameManager.GameUpdate += ZoomCamera;
    }

    private void OnDisable()
    {
        GameManager.GameStart -= StartGame;
        GameManager.GameUpdate -= CameraPan;
        GameManager.GameUpdate -= ZoomCamera;
    }

    private void CameraPan()
    {
        mainCamera.transform.position = PanVector();
    }

    private Vector3 PanVector()
    {
        Vector3 _panDirection = Vector3.zero;
        float _edgeThreshold = 5f;

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

    private void ZoomCamera() 
    {
        float _scroll = Input.GetAxis("Mouse ScrollWheel");
        if (_scroll != 0)
        {
            zoom -= _scroll * zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, zoom, ref velocity, smoothTime);
            float _camWidth = mainCamera.orthographicSize * mainCamera.aspect;
            float _camHeight = mainCamera.orthographicSize;
            mapX = 19.2f - _camWidth;
            mapY = 10.8f - _camHeight;
            Debug.Log("Zoom in");
        }

        Debug.Log($"Zoom is {zoom}, _scroll is {_scroll * zoomMultiplier}");
    }
}