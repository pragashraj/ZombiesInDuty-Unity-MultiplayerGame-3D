using UnityEngine;

public class NurseManager : MonoBehaviour
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
            case "TALKING": TalkingAnimation(true);
                break;
            default: HandleIdleAnimation();
                return;
        }
    }

    private void HandleIdleAnimation()
    {
        TalkingAnimation(false);
        TypingAnimation(false);
    }

    private void TalkingAnimation(bool active)
    {
        animator.SetBool("Talking", active);
    }

    private void TypingAnimation(bool active)
    {
        animator.SetBool("Typing", active);
    }
}
