using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void HandleWalk()
    {
        animator.SetFloat("Forward", 0.5f, 0.1f, Time.deltaTime);
    }

    public void HandleRun()
    {
        animator.SetFloat("Forward", 1f, 0.1f, Time.deltaTime);
    }

    public void HandleIdle()
    {
        animator.SetFloat("Forward", 0f, 0.1f, Time.deltaTime);
    }
}
