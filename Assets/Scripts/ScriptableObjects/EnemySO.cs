using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO",menuName = "ScriptableObjects/enemy")]
public class EnemySO : ScriptableObject
{
    public Transform enemyPrefab;
    public float health;
    public float playerDetectionRange;
    public float enemyMovementSpeed;
}
