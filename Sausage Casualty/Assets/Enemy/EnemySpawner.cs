using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab with set scaling and texturing 
    public Transform playerTransform;
    public float spawnRadius = 20f; // The radius within which enemies will be spawned
    public float initialSpawnDelay = 0f; // Initial delay before spawning enemies
    public float spawnInterval = 20f; // Time interval between enemy spawns
    public int initialEnemyCount = 2; // Initial number of enemies to spawn
    public int enemiesPerWave = 2; // Number of enemies added per wave

    private int currentWave = 1;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", initialSpawnDelay, spawnInterval);
    }

    void SpawnEnemies()
    {
        // Calculate number of enemies for this wave
        int enemiesToSpawn = initialEnemyCount + (currentWave - 1) * enemiesPerWave;

        // Spawn enemies around the player within the spawn radius
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = playerTransform.position + new Vector3(randomOffset.x, 0f, randomOffset.y);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.GetComponent<EnemyController>().SetTarget(playerTransform);
        }

        // Increment wave count
        currentWave++;
    }
}