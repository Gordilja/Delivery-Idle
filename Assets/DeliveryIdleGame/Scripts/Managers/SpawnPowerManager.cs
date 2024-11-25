using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpawnPowerManager : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int SpawnCount = 10;
    public float Radius = 5f;
    public float Timer;

    private WaitForSeconds timer;
    private List<GameObject> powerUps = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            Vector2 _randomPoint = Random.insideUnitCircle * Radius;
            var _spawnedObj = Instantiate(prefabToSpawn, new Vector3(_randomPoint.x, _randomPoint.y, 0f) + transform.position, Quaternion.identity, transform);

            _spawnedObj.SetActive(false);
            powerUps.Add(_spawnedObj);
        }
        timer = new WaitForSeconds(Timer);
        StartCoroutine(SpawnPowerUp());
    }

    public IEnumerator SpawnPowerUp() 
    {
        while (GameManager.Instance.GameState == GameState.Started)
        {
            yield return timer;
            powerUps[Random.Range(0, powerUps.Count)].SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}