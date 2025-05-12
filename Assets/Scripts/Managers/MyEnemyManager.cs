using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyManager : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject EnemyCreatePosition;

    public float StartSpawnDelay = 0f;
    public float SpawnDelay = 3f;
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", StartSpawnDelay, SpawnDelay);
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(EnemyPrefab, EnemyCreatePosition.transform.position, EnemyCreatePosition.transform.rotation);
    }
}
