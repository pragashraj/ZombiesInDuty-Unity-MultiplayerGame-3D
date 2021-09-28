using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Console : MonoBehaviour
{
    [SerializeField] private GameObject consoleUI;
    [SerializeField] private Sprite triggerImage;
    [SerializeField] private GameObject actionLoaderObj;
    [SerializeField] private GameObject door;
    [SerializeField] private string doorTag;

    private ActionLoader actionLoader;
    private AudioManager audioManager;
    private GameManager gameManager;
    private PlayerWeaponController weaponController;

    private bool onStay = false;
    private bool actionComplete = false;
    private float fillAmount = 0;

    void Start()
    {
        actionLoader = actionLoaderObj.GetComponent<ActionLoader>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    
    void Update()
    {
        if (fillAmount == 100 && !actionComplete)
        {
            actionComplete = true;
            actionLoader.FillAmount = 0;
            actionLoaderObj.GetComponent<Image>().enabled = false;
            HandleDoorAnim();
            gameManager.CompletionMessageUI("Door opened");
            weaponController.EnableWeapon();
            StartCoroutine(Reset());
        }

        if (onStay)
        {
            HandleLoader();
        }
    }

    private void HandleDoorAnim()
    {
        switch(doorTag)
        {
            case "Glass": 
                door.GetComponent<Animator>().SetBool("character_nearby", true);
                audioManager.Play("GlassDoor");
                break;
            case "Normal":
                door.transform.GetChild(0).GetComponentInChildren<Animation>().Play("LeftOpen");
                door.transform.GetChild(1).GetComponentInChildren<Animation>().Play("RightOpen");
                break;
            default: return;
        }
    }

    private void HandleLoader()
    {
        if (fillAmount != 100)
        {
            if (Input.GetKey(KeyCode.P))
            {
                consoleUI.SetActive(false);
                actionLoaderObj.SetActive(true);
                fillAmount += 2;
                actionLoader.FillAmount = fillAmount;
                weaponController.DisableWeapon();
            } else
            {
                consoleUI.SetActive(true);
                actionLoaderObj.SetActive(false);
                weaponController.EnableWeapon();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        consoleUI.GetComponent<Image>().sprite = triggerImage;

        if (other.gameObject.tag == "Player" && !actionComplete)
        {
            onStay = true;
            consoleUI.SetActive(true);
            weaponController = other.gameObject.GetComponent<PlayerWeaponController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onStay = false;
            consoleUI.SetActive(false);
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2f);
        actionLoaderObj.GetComponent<Image>().enabled = true;
        actionLoaderObj.SetActive(false);
    }
}
