
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody enemyRigidbody;
    private Vector3 direction;
    private float health = 20f;
    private int currentIndex = 0;
    private Transform[] waypoints;
    
    [SerializeField] private float enemyMovementSpeed = 2f;
    [SerializeField] private float playerDetectionRange =5f;
    private bool destinationReached;


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
        if (!destinationReached)
        {
            MoveAlongWayPoints(waypoints);
        }
        else
        {
            DetectPlayerInRange();
        }
        
    }

    public void SetWaypoints(Transform[] wayPoints)
    {
        this.waypoints = wayPoints;
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
        enemyRigidbody.MovePosition(enemyRigidbody.position + direction * (enemyMovementSpeed * Time.fixedDeltaTime));
        transform.LookAt(playerTransform);
    }
    
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void ReduceHealth(float damageAmount)
    {
        health -= damageAmount;
        if(health <= 0) DestroySelf();
    }

    public void MoveAlongWayPoints(Transform[] wayPoints)
    {
        if(wayPoints.Length == 0) return;
        if (currentIndex >= waypoints.Length)
        {
            destinationReached = true;
            return;
        }
        Transform target= wayPoints[currentIndex];
        direction = (target.position - transform.position).normalized;
        transform.LookAt(target);
        if (Vector3.Distance(transform.position,target.position)>1f)
        {
            enemyRigidbody.MovePosition(enemyRigidbody.position + direction * (enemyMovementSpeed * Time.fixedDeltaTime));
        }
        else
        {
            currentIndex++;
        }
        
    }

}
