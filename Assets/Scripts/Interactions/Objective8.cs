using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Objective8 : MonoBehaviour
{
    [SerializeField] private GameObject[] quads;
    [SerializeField] private Vector3[] pos;
    [SerializeField] private GameObject triggerUI;
    [SerializeField] private Sprite triggerImage;

    private GameManager gameManager;
    private AudioManager audioManager;

    private bool onStay;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }


    void Update()
    {
        if (onStay)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                HandleCompletion();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerUI.GetComponent<Image>().sprite = triggerImage;

        bool objective7 = gameManager.Objective7Completed;
        bool objective8 = gameManager.Objective8Completed;

        if (other.gameObject.tag == "Player")
        {
            if (objective7 && !objective8)
            {
                onStay = true;
                triggerUI.SetActive(true);
            }
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

    private void HandleCompletion()
    {
        triggerUI.SetActive(false);
        gameObject.transform.position = new Vector3(0, -15, 0);
        audioManager.Play("Collect");
        gameManager.Objective8Completed = true;
        gameManager.HandleCompletionUI("Objective 8 completed");

        for (int i = 0; i < quads.Length; i++)
        {
            if (i == 0 || i == 1)
            {
                quads[i].transform.position = pos[i];
            }
            else
            {
                quads[i].SetActive(false);
            }
        }
        StartCoroutine(NextMessage());
    }

    IEnumerator NextMessage()
    {
        yield return new WaitForSeconds(4f);
        gameManager.HandleCompletionUI("Ok now clear the area!");
        gameObject.SetActive(false);
    }
}
