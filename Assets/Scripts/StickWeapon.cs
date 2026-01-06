
using UnityEngine;

public class StickWeapon : MonoBehaviour
{
    public enum AttackType
    {
        Normal,
        Heavy
    }
    
    [SerializeField] private float hitForce;
    private Rigidbody stickWeaponRigidbody;
    private float damageAmount;
    private float normalAttackDamage = 10f;
    private float heavyAttackDamageAmount = 20f;
    
    private AttackType attackType;
    
    private void Awake()
    {
        stickWeaponRigidbody =  GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameInputs.Instance.IsAttackPressed() && transform.parent)
        {
            attackType = AttackType.Normal;
        }
        if (GameInputs.Instance.IsHeavyAttackPressed() && transform.parent)
        {
            attackType = AttackType.Heavy;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (attackType == AttackType.Normal) damageAmount = normalAttackDamage;
            if (attackType == AttackType.Heavy) damageAmount = heavyAttackDamageAmount;
            Vector3 hitDir = collision.contacts[0].normal * -1f;
            collision.rigidbody.AddForce(hitDir * hitForce, ForceMode.Impulse);
            enemy.ReduceHealth(damageAmount);
        }
        if (collision.gameObject.TryGetComponent(out EnemyTank enemyTank))
        {
            if (attackType == AttackType.Normal) damageAmount = normalAttackDamage;
            if (attackType == AttackType.Heavy) damageAmount = heavyAttackDamageAmount;
            Vector3 hitDir = collision.contacts[0].normal * -1f;
            collision.rigidbody.AddForce(hitDir * hitForce, ForceMode.Impulse);
            enemyTank.ReduceHealth(damageAmount);
        }
    }
    
}
