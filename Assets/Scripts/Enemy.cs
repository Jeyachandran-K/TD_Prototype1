using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody enemyRigidbody;
    private Vector3 direction;
    
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
        MoveEnemy(direction);
    }
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
    

    private void DetectPlayerInRange()
    {
        if(!playerTransform) return;
        float playerDistance  = Vector3.Distance(transform.position, playerTransform.position);
        if (playerDistance > playerDetectionRange) return;
        direction = (playerTransform.position - transform.position).normalized;
        transform.LookAt(playerTransform);
    }

    public void MoveEnemy(Vector3 direction)
    {
        enemyRigidbody.MovePosition(enemyRigidbody.position + direction * (enemyMovementSpeed * Time.fixedDeltaTime));
    }
    
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    
}
