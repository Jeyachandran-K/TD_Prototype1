using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody enemyRigidbody;
    
    [SerializeField] private float enemyMovementSpeed;
    [SerializeField] private float playerDetectionRange;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        playerTransform = FindAnyObjectByType<Player>().transform;
    }

    private void FixedUpdate()
    {
        DetectPlayerInRange();
    }
    

    private void DetectPlayerInRange()
    {
        if(!playerTransform) return;
        float playerDistance  = Vector3.Distance(transform.position, playerTransform.position);
        if (playerDistance > playerDetectionRange) return;
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        MoveEnemy(direction);
        transform.LookAt(playerTransform);
    }

    public void MoveEnemy(Vector3 direction)
    {
        enemyRigidbody.MovePosition(enemyRigidbody.position + direction * (enemyMovementSpeed * Time.fixedDeltaTime));
    }
    
    public Rigidbody GetEnemyRigidbody()
    {
        return enemyRigidbody;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public float GetEnemyMovementSpeed()
    {
        return enemyMovementSpeed;
    }
}
