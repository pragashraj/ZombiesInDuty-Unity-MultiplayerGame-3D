using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject mainMenuControl;
    [SerializeField] private GameObject gameEndMenu;
    [SerializeField] private GameObject levelEndMenu;

    private bool isMainMenuOpen;
    private PlayerWeaponController weaponController;

    private AudioManager audioManager;

    void Start()
    {
        weaponController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeaponController>();
        audioManager = FindObjectOfType<AudioManager>();
        Cursor.visible = false;
    }

    
    void Update()
    {
        HandleMainMenu();
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    private void HandleMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMainMenuOpen = !isMainMenuOpen;

            if (isMainMenuOpen)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                weaponController.enabled = false;
            } 
            else
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                weaponController.enabled = true;
            }

            HandleMainMenuActive(isMainMenuOpen);
            Cursor.visible = isMainMenuOpen;
        }
    }

    private void HandleMainMenuActive(bool active)
    {
        mainMenu.SetActive(active);
    }

    private void HandleMainMenuControlActive(bool active)
    {
        mainMenuControl.SetActive(active);
    }

    public void HandleGameEndMenuActive(bool active)
    {
        gameEndMenu.SetActive(active);
    }

    public void HandleLevelEndMainMenuActive(bool active)
    {
        levelEndMenu.SetActive(active);
    }

    public void HandleControlsOnClick()
    {
        PlayAudio("Click");
        HandleMainMenuActive(false);
        HandleMainMenuControlActive(true);
    }

    public void HandleNewGameOnClick()
    {
        PlayAudio("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HandleContinueOnClick()
    {
        PlayAudio("Click");
        HandleMainMenuActive(false);
        HandleMainMenuControlActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weaponController.enabled = true;
    }

    public void HandleQuit()
    {
        PlayAudio("Click");
        Application.Quit();
    }
}
