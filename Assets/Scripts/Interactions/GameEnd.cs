using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private GameManager gameManager;
    private MenuManager menuManager;
    private PlayerWeaponController weaponController;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        menuManager = FindObjectOfType<MenuManager>();
        weaponController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeaponController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool objective10 = gameManager.Objective10Completed;

        if (other.gameObject.tag == "Player" && objective10)
        {
            weaponController.enabled = false;
            gameManager.HandleCompletionUI("Level completed");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            menuManager.HandleLevelEndMainMenuActive(true);
        }
    }
}
