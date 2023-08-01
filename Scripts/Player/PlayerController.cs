
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TargetHandler targetHandler;
    public TargetHandler TargetHandler => targetHandler;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        targetHandler = GameObject.Find("TargetHandler").GetComponent<TargetHandler>();
    }

    private void Update()
    {
        if (targetHandler.Target == null)
        {
            animator.SetInteger("AttackState", 0);
            return; 
        }
        transform.LookAt(new Vector3(targetHandler.Target.transform.position.x, transform.position.y, targetHandler.Target.transform.position.z));
        animator.SetInteger("AttackState", 1);
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .59f)
        {
            if (targetHandler.Target == null)
            {
                animator.SetInteger("AttackState", 0);
            }
            else
            {
                animator.SetInteger("AttackState", 2);
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.speed = PropertyManager.instance.AttackSpeed;
        }
        else
        {
            animator.speed = 1;
        }
    }
}
