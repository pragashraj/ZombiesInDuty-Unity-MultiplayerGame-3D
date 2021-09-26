using UnityEngine;
using UnityEngine.AI;

public class ThirdPersonCharacterControl : MonoBehaviour
{
    private enum State
    {
        START, IDLE, WALKING, RUNNING
    }

    [SerializeField] private float walkingSpeed;
    [SerializeField] private float speedMultiplyer = 2.5f;

    private float speed;
    private State state = State.START;

    private PlayerAnimatorController animatorController;

    private void Start()
    {
        animatorController = GetComponent<PlayerAnimatorController>();
        state = State.IDLE;
        speed = walkingSpeed;
    }

    private void Update()
    {
        PlayerMovement();
        speed = state == State.WALKING ? walkingSpeed : walkingSpeed * speedMultiplyer;
    }

    void PlayerMovement()
    {
        float ver = Input.GetAxis("Vertical");

        if (ver > 0 && !Input.GetKey(KeyCode.LeftShift))
        {
            state = State.WALKING;
            animatorController.HandleWalk();
        }
        else if (ver > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            state = State.RUNNING;
            animatorController.HandleRun();
        }
        else
        {
            state = State.IDLE;
            animatorController.HandleIdle();
        }
    }
}