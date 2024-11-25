using UnityEditor;
using UnityEngine;
public class CursorManager : MonoBehaviour
{
    private Vector3 offset = new Vector3(-15f, 44f, 0);

#if UNITY_EDITOR
    void Start()
    {
        Cursor.SetCursor(PlayerSettings.defaultCursor, Vector2.zero, CursorMode.Auto);
    }
#endif
}