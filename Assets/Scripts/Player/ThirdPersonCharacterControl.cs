using UnityEngine;

public class ThirdPersonCharacterControl : MonoBehaviour
{
    private PlayerAnimatorController animatorController;

    private void Start()
    {
        animatorController = GetComponent<PlayerAnimatorController>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float ver = Input.GetAxis("Vertical");

        if (ver > 0 && !Input.GetKey(KeyCode.LeftShift))
        {
            animatorController.HandleWalk(0.5f);
        }
        else if (ver > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            animatorController.HandleRun();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            animatorController.WalkHorizontal(0.5f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animatorController.WalkHorizontal(-0.5f);
        }
        else
        {
            animatorController.HandleIdle();
        }
    }
}