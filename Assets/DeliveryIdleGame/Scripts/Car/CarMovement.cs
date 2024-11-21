using System.Collections;
using System.Collections.Generic;
using QPathFinder;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public PathFinder PathFinder;
    public PathFollower PathFollower;
    public int Speed;
    public CarState CarState;

    private WaitForSeconds speedBoost = new WaitForSeconds(3);

    [HideInInspector] public int fromNode = 1;
    [HideInInspector] public int toNode = 1;

    private void OnEnable()
    {
        GameManager.CarStop += StoppedCard;
    }

    private void OnDisable()
    {
        GameManager.CarStop -= StoppedCard;
    }

    public void SetNewDestination(int _destination)
    {
        fromNode = toNode;
        toNode = _destination;
        PathFinder.FindShortestPathOfNodes(fromNode, toNode, Execution.Synchronous, OnPathFound);

        GameManager.Instance.AdressList.RemoveAt(0);
        CarState = CarState.Running;
    }

    public void SetSpeed(int _add) 
    {
        StartCoroutine(SpeedBoostEvent(_add));
    }

    private IEnumerator SpeedBoostEvent(int _add) 
    {
        Speed += _add;
        yield return speedBoost;
        Speed -= _add;
    }

    private void OnPathFound(List<Node> nodes) 
    {
        PathFollowerUtility.FollowPath(this.transform, nodes, Speed, false);
    }

    private void StoppedCard() 
    {

        CarState = CarState.Stopped;

        if (GameManager.Instance.AdressList.Count > 0) 
        {
            SetNewDestination(GameManager.Instance.AdressList[0].NodeIndex);
        }
    }
}