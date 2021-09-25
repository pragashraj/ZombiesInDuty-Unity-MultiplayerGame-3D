using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Objective2 : MonoBehaviour
{

    [SerializeField] private GameObject triggerUI;
    [SerializeField] private Sprite triggerImage;
    [SerializeField] private GameObject actionLoaderObj;

    private bool onStay;
    private bool actionComplete;
    private bool audioStarted;
    private float fillAmount = 0;

    private GameManager gameManager;
    private ActionLoader actionLoader;
    private AudioManager audioManager;

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
            gameManager.Objective2Completed = true;
            gameManager.HandleCompletionUI("Objective 2 completed");
            audioManager.Stop("Typing");
            actionLoader.FillAmount = 0;
            actionLoaderObj.GetComponent<Image>().enabled = false;
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

        bool objective1 = gameManager.Objective1Completed;
        bool objective2 = gameManager.Objective2Completed;

        if (other.gameObject.tag == "Player" && objective1 && !objective2)
        {
            onStay = true;
            triggerUI.SetActive(true);
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
                fillAmount += 2;
                actionLoader.FillAmount = fillAmount;
            }
            else
            {
                audioManager.Stop("Typing");
                actionLoaderObj.SetActive(false);
                triggerUI.SetActive(!actionComplete);
                audioStarted = false;
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
