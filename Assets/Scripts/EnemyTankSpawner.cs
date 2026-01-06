using UnityEngine;

public class EnemyTankSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyTankSpawnPoint;
    [SerializeField] private Transform enemyTargetTransform;
    
    private readonly float spawnInterval = 5f;
    private float timer;
    private Vector3 direction;

    private void Start()
    {
        SpawnEnemy(enemyTankSpawnPoint,225);
    }
    private void Update()
    {
        if (timer >= spawnInterval)
        {
            timer = 0;
            SpawnEnemy(enemyTankSpawnPoint,225);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void SpawnEnemy(Transform spawnPoint,float rotation)
    {
        GameObject spawnedEnemy = Instantiate(enemyPrefab,spawnPoint.position, Quaternion.Euler(0f,rotation,0f));
        EnemyTank enemyTank = spawnedEnemy.GetComponent<EnemyTank>();
        Vector3 direction = (enemyTargetTransform.position - spawnedEnemy.transform.position).normalized;
        enemyTank.SetDirection(direction);
    }

}
