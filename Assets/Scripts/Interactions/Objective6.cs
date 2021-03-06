using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Objective6 : MonoBehaviour
{
    [SerializeField] private GameObject triggerUI;
    [SerializeField] private Sprite triggerImage;
    [SerializeField] private GameObject actionLoaderObj;

    private GameManager gameManager;
    private ActionLoader actionLoader;
    private AudioManager audioManager;
    private PlayerWeaponController weaponController;

    private bool onStay;
    private bool actionComplete;
    private bool audioStarted;
    private float fillAmount = 0;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        actionLoader = actionLoaderObj.GetComponent<ActionLoader>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }


    void Update()
    {
        if (fillAmount == 100 && !actionComplete)
        {
            actionComplete = true;
            actionLoader.FillAmount = 0;
            actionLoaderObj.GetComponent<Image>().enabled = false;
            gameManager.Objective6Completed = true;
            gameManager.HandleCompletionUI("Objective 6 completed");
            audioManager.Stop("Typing");
            weaponController.EnableWeapon();
            StartCoroutine(Reset());
        }

        if (onStay)
        {
            HandleLoader();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerUI.GetComponent<Image>().sprite = triggerImage;

        bool objective5 = gameManager.Objective5Completed;
        bool objective6 = gameManager.Objective6Completed;

        if (other.gameObject.tag == "Player" && objective5 && !objective6)
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
                fillAmount += 1;
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

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2f);
        actionLoaderObj.GetComponent<Image>().enabled = true;
        actionLoaderObj.SetActive(false);
    }
}
