using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform leftEnemySpawner;
    [SerializeField] private Transform rightEnemySpawner;
    [SerializeField] private Transform enemyTargetTransform;
    
    private readonly float spawnInterval = 2f;
    private float timer;

    private void Update()
    {
        if (timer >= spawnInterval)
        {
            timer = 0;
            SpawnEnemy(leftEnemySpawner);
            // SpawnEnemy(rightEnemySpawner);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void SpawnEnemy(Transform spawnPoint)
    {
        GameObject spawnedEnemy = Instantiate(enemyPrefab,spawnPoint.position, Quaternion.identity);
        
        Vector3 direction = (enemyTargetTransform.position - spawnedEnemy.transform.position).normalized;
        MoveSpawnedEnemy(spawnedEnemy, direction);
    }

    private void MoveSpawnedEnemy(GameObject spawnedEnemy,Vector3 direction)
    {
        Enemy enemy = spawnedEnemy.GetComponent<Enemy>();
        enemy.GetEnemyRigidbody().MovePosition(enemy.GetEnemyRigidbody().position + direction * ((enemy.GetEnemyMovementSpeed()) * Time.deltaTime));
        // enemy.MoveEnemy(direction);
    }
    
}
