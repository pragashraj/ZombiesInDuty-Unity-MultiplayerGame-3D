using UnityEngine;

public class CanibalManager : MonoBehaviour
{
    [SerializeField] private string animatorType;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ControlAnimations();
    }

    private void ControlAnimations()
    {
        switch(animatorType)
        {
            case "IDLE":
                HandleAnimation(0);
                break;
            case "ATTACK":
                HandleAnimation(0.75f);
                break;
            default:
                HandleAnimation(0);
                return;
        }
    }

    private void HandleAnimation(float value)
    {
        animator.SetFloat("Movement", value, 0.1f, Time.deltaTime);
    }
}
