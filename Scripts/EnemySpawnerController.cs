using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] waypoints;
    private int totalEnemies = 15;
    private int enemiesSpawned = 0;
    private float spawnDelay = 5f;

    void Start()
    {
        StartCoroutine(SpawnEnemyShipsRoutine());
    }

    IEnumerator SpawnEnemyShipsRoutine()
    {
        while (enemiesSpawned < totalEnemies)
        {
            yield return new WaitForSeconds(spawnDelay);

            if (enemiesSpawned == 0)
            {
                Vector3 spawnPosition = waypoints[0].position;
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.waypoints = waypoints;
                }
            }
            else
            {
                GameObject newEnemy = Instantiate(enemyPrefab, waypoints[0].position, Quaternion.identity);
                EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.waypoints = waypoints;
                }
            }

            enemiesSpawned++;
        }
    }
}