using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawner : MonoBehaviour
{
    public Wave[] waves;
    public NewEnemy enemy;

    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    float nextSpawnTime;
    int enemiesRemainingAlive;

    void Start()
    {
        NextWave();
    }

    void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            NewEnemy spawnedEnemy = Instantiate(enemy, Vector3.zero, enemy.transform.rotation) as NewEnemy;
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;

        if (enemiesRemainingAlive ==0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        if (currentWaveNumber -1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
        
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }
}
