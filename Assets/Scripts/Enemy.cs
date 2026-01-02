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
        if(playerTransform == null) return;
        float distance  = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= playerDetectionRange)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            enemyRigidbody.MovePosition(enemyRigidbody.position + direction * (enemyMovementSpeed * Time.fixedDeltaTime));
            transform.LookAt(playerTransform);
        }
    }
}
