using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; //The enemy prefab with set scaling and texturing 
    public Transform playerTransform;
    public int initialEnemyCount = 2; //The count for the first wave of enemies
    public float spawnRadius = 20f; //Not sure of the exact radius of arena, this will need tweaked
    public float waveCooldown = 5f; //A five second cooldown between waves
    public float initialSpawnDelay = 0f;
    public float spawnInterval = 20f;
    public int enemiesPerWave = 2;

    private int currentWave = 1;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", initialSpawnDelay, spawnInterval);
    }


    void SpawnEnemies()
    {
        int enemiesToSpawn = initialEnemyCount + (currentWave - 1) * enemiesPerWave;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = playerTransform.position + new Vector3(randomOffset.x, 0f, randomOffset.y);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.GetComponent<EnemyController>().SetTarget(playerTransform);
        }

        currentWave++;
    }
}
