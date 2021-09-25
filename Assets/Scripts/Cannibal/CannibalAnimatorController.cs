using UnityEngine;

public class CannibalAnimatorController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Walk(bool active)
    {
        animator.SetBool("Walk", active);
    }

    public void Run(bool active)
    {
        animator.SetBool("Run", active);
    }

    public void Attack(bool active)
    {
        animator.SetBool("Attack", active);
    }

    public void PunchLeft(bool active)
    {
        animator.SetBool("PunchLeft", active);
    }

    public void PunchRight(bool active)
    {
        animator.SetBool("PunchRight", active);
    }

    public void HitNormal()
    {
        animator.SetTrigger("HitNormal");
    }

    public void HitHard()
    {
        animator.SetTrigger("HitHard");
    }
}
