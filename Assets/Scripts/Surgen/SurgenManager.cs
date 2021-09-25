using UnityEngine;

public class SurgenManager : MonoBehaviour
{
    [SerializeField] private string animatorType;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        ControlAnimations();
    }

    private void ControlAnimations()
    {
        switch (animatorType)
        {
            case "IDLE": HandleIdleAnimation();
                break;
            case "TYPING": TypingAnimation(true);
                break;
            case "ARGUING": ArguingAnimation(true);
                break;
            case "LAYING": LayingAnimation(true);
                break;
            default: HandleIdleAnimation();
                return;
        }
    }

    private void HandleIdleAnimation()
    {
        TypingAnimation(false);
        ArguingAnimation(false);
        LayingAnimation(false);
    }

    private void ArguingAnimation(bool active)
    {
        animator.SetBool("Arguing", active);
    }

    private void TypingAnimation(bool active)
    {
        animator.SetBool("Typing", active);
    }

    private void WalkingAnimation(bool active)
    {
        animator.SetBool("Walking", active);
    }

    private void LayingAnimation(bool active)
    {
        animator.SetBool("Laying", active);
    }
}
