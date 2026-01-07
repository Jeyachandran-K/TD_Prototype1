using System;
using Interfaces;
using UnityEngine;

public class BasicEnemy : MonoBehaviour,IEnemy
{
    [SerializeField] private EnemySO enemySO;

    protected Transform playerTransform;
    protected Rigidbody enemyRigidbody;

    private void Awake()
    {
        playerTransform= FindAnyObjectByType<Player>().transform;
        enemyRigidbody =  GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        DetectPlayer();
    }

    public void TakeDamage(float damageAmount)
    {
        
    }

    public void MoveAlongWayPoints(Transform[] wayPoints)
    {
        //noop
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void DetectPlayer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= enemySO.playerDetectionRange)
        {
            ChasePlayer();
        }
    }

    public void ChasePlayer()
    {
        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;
        enemyRigidbody.MovePosition(enemyRigidbody.position+ playerDirection * (enemySO.enemyMovementSpeed * Time.fixedDeltaTime));
        transform.LookAt(playerTransform);
    }
}
