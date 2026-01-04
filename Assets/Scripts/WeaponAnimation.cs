
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    private static readonly int OnAttack = Animator.StringToHash("OnAttack");
    private static readonly int OnHeavyAttack = Animator.StringToHash("OnHeavyAttack");
    private Animator  animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameInputs.Instance.IsAttackPressed() && transform.parent)
        {
            animator.SetTrigger(OnAttack);    
        }

        if (GameInputs.Instance.IsHeavyAttackPressed() && transform.parent)
        {
            animator.SetTrigger(OnHeavyAttack);
        }
    }
}
