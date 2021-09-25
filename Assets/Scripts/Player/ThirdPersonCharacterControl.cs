using UnityEngine;

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

    private void Start()
    {
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
        }
        else if (ver > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            state = State.RUNNING;
        }
        else
        {
            state = State.IDLE;
        }

        Vector3 playerMovement = new Vector3(0f, 0f, ver) * speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }
}