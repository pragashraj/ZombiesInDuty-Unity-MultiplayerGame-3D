using UnityEngine;
using System.Collections;

public class Objective5 : MonoBehaviour
{
    [SerializeField] private GameObject triggerUI;

    private GameManager gameManager;
    private AudioManager audioManager;
    private PlayerWeaponController weaponController;

    private bool onStay;
    private bool actionComplete;
    private bool audioStarted;
    private float fillAmount = 0;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    
    void Update()
    {
        if (fillAmount == 100 && !actionComplete)
        {
            audioManager.Stop("Lawn");
            triggerUI.SetActive(false);
            actionComplete = true;
            gameManager.Objective5Completed = true;
            gameManager.HandleCompletionUI("Objective 5 completed");
            triggerUI.GetComponent<Animation>().Stop("Wheeling");
            StartCoroutine(AfterCompletion());
        }

        if (onStay)
        {
            HandleLoader();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool objective4 = gameManager.Objective4Completed;
        bool objective5 = gameManager.Objective5Completed;

        if (other.gameObject.tag == "Player" && objective4 && !objective5)
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
                    audioManager.Play("Lawn");
                    audioStarted = true;
                }
                triggerUI.GetComponent<Animation>().Play("Wheeling");
                fillAmount += 2;
                weaponController.DisableWeapon();
            }
            else
            {
                audioManager.Stop("Lawn");
                triggerUI.SetActive(!actionComplete);
                audioStarted = false;
                triggerUI.GetComponent<Animation>().Stop("Wheeling");
                weaponController.EnableWeapon();
            }
        }
    }


    IEnumerator AfterCompletion()
    {
        yield return new WaitForSeconds(4f);
        gameManager.CompletionMessageUI("Ok, Get backups now");
    }
}
