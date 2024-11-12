using System.Collections.Generic;
using QPathFinder;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public PathFinder PathFinder;
    public PathFollower PathFollower;
    public int Speed;

    public int fromNode;
    public int toNode;

    public void SetNewDestination()
    {
        PathFinder.FindShortestPathOfNodes(fromNode, toNode, Execution.Synchronous, OnPathFound);
    }

    public void SetSpeed() 
    {
        PathFollower.SetSpeed(Speed);
    }

    private void OnPathFound(List<Node> nodes) 
    {
        PathFollowerUtility.FollowPath(this.transform, nodes, Speed, false);
    }
}