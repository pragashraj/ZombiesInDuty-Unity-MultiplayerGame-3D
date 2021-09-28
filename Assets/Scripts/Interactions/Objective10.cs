using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Objective10 : MonoBehaviour
{
    [SerializeField] private GameObject triggerUI;
    [SerializeField] private Sprite triggerImage;
    [SerializeField] private GameObject actionLoaderObj;
    [SerializeField] private GameObject door;

    private ActionLoader actionLoader;
    private AudioManager audioManager;
    private GameManager gameManager;
    private PlayerWeaponController weaponController;

    private bool onStay = false;
    private float fillAmount = 0;
    private bool actionComplete;
    private bool audioStarted;

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
            gameManager.HandleCompletionUI("Objective 10 completed");
            gameManager.Objective10Completed = true;
            weaponController.EnableWeapon();
            audioManager.Stop("Typing");
            StartCoroutine(Reset());
        }

        if (onStay)
        {
            HandleLoader();
        }
    }

    private void HandleDoorAnim()
    {
        door.transform.GetChild(0).GetComponentInChildren<Animation>().Play("LeftOpen");
        door.transform.GetChild(1).GetComponentInChildren<Animation>().Play("RightOpen");
    }

    private void HandleLoader()
    {
        if (fillAmount != 100)
        {
            if (Input.GetKey(KeyCode.P))
            {
                if (!audioStarted)
                {
                    audioManager.Play("Typing");
                    audioStarted = true;
                }

                triggerUI.SetActive(false);
                actionLoaderObj.SetActive(true);
                fillAmount += 2;
                actionLoader.FillAmount = fillAmount;
                weaponController.DisableWeapon();
            }
            else
            {
                audioManager.Stop("Typing");
                actionLoaderObj.SetActive(false);
                triggerUI.SetActive(!actionComplete);
                audioStarted = false;
                weaponController.EnableWeapon();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerUI.GetComponent<Image>().sprite = triggerImage;

        bool objective9 = gameManager.Objective9Completed;
        bool objective10 = gameManager.Objective10Completed;

        if (other.gameObject.tag == "Player" && objective9 && !objective10)
        {
            onStay = true;
            triggerUI.SetActive(true);
            weaponController = other.gameObject.GetComponent<PlayerWeaponController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onStay = false;
            triggerUI.SetActive(false);
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2f);
        actionLoaderObj.GetComponent<Image>().enabled = true;
        actionLoaderObj.SetActive(false);
    }
}
