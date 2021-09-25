using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void WalkAnim(bool value)
    {
        animator.SetBool("walk", value);
    }

    public void RunAnim(bool active)
    {
        animator.SetBool("run", active);
    }

    public void Idle()
    {
        animator.SetTrigger("idle");
    }

    public void Jump (bool active)
    {

    }
}
