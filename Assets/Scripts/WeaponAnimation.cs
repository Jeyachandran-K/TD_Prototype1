
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    private static readonly int OnAttack = Animator.StringToHash("OnAttack");
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
    }
}
