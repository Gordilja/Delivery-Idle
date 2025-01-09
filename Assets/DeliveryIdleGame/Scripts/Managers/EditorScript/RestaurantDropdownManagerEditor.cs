using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(RestaurantDropdownManager))]
public class RestaurantDropdownManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector
        DrawDefaultInspector();

        // Add a button
        RestaurantDropdownManager myScript = (RestaurantDropdownManager)target;
        if (GUILayout.Button("Populate Dropdown"))
        {
            myScript.FillButtonList();
        }
    }
}
#endif