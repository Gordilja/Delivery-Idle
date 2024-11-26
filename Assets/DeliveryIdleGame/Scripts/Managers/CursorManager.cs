using UnityEngine;
public class CursorManager : MonoBehaviour
{
    private Vector3 offset = new Vector3(-15f, 44f, 0);
#if UNITY_EDITOR

    private void Update()
    {
        transform.position = Input.mousePosition - offset;
    }
#endif
}