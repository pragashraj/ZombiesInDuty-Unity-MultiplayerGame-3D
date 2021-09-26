using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void HandleWalk(float value)
    {
        animator.SetFloat("Forward", value, 0.1f, Time.deltaTime);
    }

    public void HandleRun()
    {
        animator.SetFloat("Forward", 1f, 0.1f, Time.deltaTime);
    }

    public void HandleIdle()
    {
        animator.SetFloat("Forward", 0f, 0.1f, Time.deltaTime);
        animator.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
    }

    public void WalkHorizontal(float value)
    {
        animator.SetFloat("Horizontal", value, 0.1f, Time.deltaTime);
    }

    public void RunHoriZontal(float value)
    {
        animator.SetFloat("Horizontal", value, 0.1f, Time.deltaTime);
    }
}
