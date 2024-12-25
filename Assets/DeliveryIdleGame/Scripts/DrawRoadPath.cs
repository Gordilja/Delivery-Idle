using QPathFinder;
using System.Collections.Generic;
using UnityEngine;

public class DrawRoadPath : MonoBehaviour
{
    public static DrawRoadPath Instance;   

    [SerializeField] private LineRenderer lineRenderer;
    private List<Vector3> pathPoints = new List<Vector3>(); // Define points for the path

    void Awake()
    {
        Instance = this;
    }

    private void DrawPath()
    {
        // Set positions for LineRenderer
        for (int i = 0; i < pathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, pathPoints[i]);
        }
    }

    // Converts a list of Nodes to Vector3 and updates the path
    public void SetPathFromNodes(List<Node> nodes)
    {
        pathPoints.Clear();
        foreach (var node in nodes)
        {
            pathPoints.Add(new Vector3(node.Position.x, node.Position.y, 0)); // Convert Node to Vector3
        }
        lineRenderer.positionCount = pathPoints.Count;
        DrawPath();
    }
}