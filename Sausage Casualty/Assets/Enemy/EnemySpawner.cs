using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; //The enemy prefab with set scaling and texturing 
    public Transform playerTransform;
    public int startingEnemies = 5; //The count for the first wave of enemies
    public float spawnRadius = 20f; //Not sure of the exact radius of arena, this will need tweaked
    public float waveCooldown = 5f; //A five second cooldown between waves

    private int currentWave = 1;
    private int enemiesToSpawn;

    void Start()
    {
        enemiesToSpawn = startingEnemies;
        SpawnEnemies();
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Invoke("StartNextWave", waveCooldown);
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 randomPosition = playerTransform.position + Random.insideUnitSphere * spawnRadius;
            randomPosition.y = 0f; //Spawning enemies at ground level 
            GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            enemy.GetComponent<EnemyController>().SetTarget(playerTransform);
        }
    }

    void StartNextWave()
    {
        currentWave++;
        enemiesToSpawn += 2;
        SpawnEnemies();
    }
}
