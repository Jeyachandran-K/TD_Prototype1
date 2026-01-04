using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform leftEnemySpawner;
    [SerializeField] private Transform rightEnemySpawner;
    [SerializeField] private Transform enemyTargetTransform;
    
    private readonly float spawnInterval = 3f;
    private float timer;
    private Vector3 direction;

    private void Start()
    {
        SpawnEnemy(leftEnemySpawner,90);
        SpawnEnemy(rightEnemySpawner,270);
    }
    private void Update()
    {
        if (timer >= spawnInterval)
        {
            timer = 0;
            SpawnEnemy(leftEnemySpawner,90);
            SpawnEnemy(rightEnemySpawner,270);
            // SpawnEnemy(rightEnemySpawner);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void SpawnEnemy(Transform spawnPoint,float rotation)
    {
        GameObject spawnedEnemy = Instantiate(enemyPrefab,spawnPoint.position, Quaternion.Euler(0f,rotation,0f));
        Enemy enemy = spawnedEnemy.GetComponent<Enemy>();
        Vector3 direction = (enemyTargetTransform.position - spawnedEnemy.transform.position).normalized;
        enemy.SetDirection(direction);
    }
    
}
