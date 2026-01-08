
using Interfaces;
using UnityEngine;

public class StickWeapon : MonoBehaviour,IPickable
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
        
        if (collision.gameObject.TryGetComponent(out IEnemy enemy))
        {
            damageAmount=GetDamageAmount();
            Vector3 hitDir = collision.contacts[0].normal * -1f;
            collision.rigidbody.AddForce(hitDir * hitForce, ForceMode.Impulse);
            enemy.TakeDamage(damageAmount);
        }
        
    }

    private float GetDamageAmount()
    {
        if (attackType == AttackType.Normal) damageAmount = normalAttackDamage;
        if (attackType == AttackType.Heavy) damageAmount = heavyAttackDamageAmount;
        return damageAmount;
    }

    public Vector3 GetLocalPositionVector()
    {
        return new Vector3(0.3f, -0.25f, -0.5f);
    }
}
