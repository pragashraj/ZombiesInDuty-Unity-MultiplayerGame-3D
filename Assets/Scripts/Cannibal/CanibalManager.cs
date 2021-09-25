using UnityEngine;

public class CanibalManager : MonoBehaviour
{
    [SerializeField] private string animatorType;

    private CannibalAnimatorController cannibalAnimatorCotroller;

    void Start()
    {
        cannibalAnimatorCotroller = GetComponent<CannibalAnimatorController>();
    }

    
    void Update()
    {
        ControlAnimations();
    }

    private void ControlAnimations()
    {
        switch(animatorType)
        {
            case "IDLE": HandleIdleAnimation();
                break;
            case "ATTACK": HandleAttackAnimation();
                break;
            default: HandleIdleAnimation();
                return;
        }
    }

    private void HandleIdleAnimation()
    {
        cannibalAnimatorCotroller.Walk(false);
        cannibalAnimatorCotroller.Run(false);
        cannibalAnimatorCotroller.Attack(false);
        cannibalAnimatorCotroller.PunchLeft(false);
        cannibalAnimatorCotroller.PunchRight(false);
    }

    private void HandleWalkAnimation()
    {
        cannibalAnimatorCotroller.Walk(true);
    }

    private void HandleRunAnimation()
    {
        cannibalAnimatorCotroller.Run(true);
    }

    private void HandleAttackAnimation()
    {
        cannibalAnimatorCotroller.Attack(true);
    }

    private void HandlePunchLeftAnimation()
    {
        cannibalAnimatorCotroller.PunchLeft(true);
    }

    private void HandlePunchRightAnimation()
    {
        cannibalAnimatorCotroller.PunchRight(true);
    }

    private void HandleHitNormalAnimation()
    {
        cannibalAnimatorCotroller.HitNormal();
    }

    private void HandleHitHardAnimation()
    {
        cannibalAnimatorCotroller.HitHard();
    }
}
